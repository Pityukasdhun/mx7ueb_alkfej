import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { BooksApiService, Book } from '../../api/books-api.service';

@Component({
  selector: 'app-books',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './books.component.html'
})
export class BooksComponent {
  books: Book[] = [];
  loading = false;
  error: string | null = null;

  title = '';
  author = '';
  year = new Date().getFullYear();

  q = '';

  page = 1;
  pageSize = 5;

  constructor(private api: BooksApiService) {}

  ngOnInit(): void {
    this.load();
  }

  get totalPages(): number {
    return Math.max(1, Math.ceil(this.books.length / this.pageSize));
  }

  get pagedBooks(): Book[] {
    const start = (this.page - 1) * this.pageSize;
    return this.books.slice(start, start + this.pageSize);
  }

  load(): void {
    this.loading = true;
    this.error = null;

    this.api.list(this.q).subscribe({
      next: (data: Book[]) => {
        this.books = data ?? [];
        this.page = 1;
        this.loading = false;
      },
      error: (err: unknown) => {
        this.error = 'Failed to load books.';
        this.loading = false;
        console.error(err);
      }
    });
  }

  create(): void {
    const t = this.title.trim();
    const a = this.author.trim();

    if (!t || !a) {
      this.error = 'Title and author are required.';
      return;
    }

    this.api.create({ title: t, author: a, year: this.year }).subscribe({
      next: () => {
        this.title = '';
        this.author = '';
        this.year = new Date().getFullYear();
        this.load();
      },
      error: (err: unknown) => {
        this.error = 'Failed to create book.';
        console.error(err);
      }
    });
  }

  remove(id?: string): void {
    if (!id) return;

    this.api.delete(id).subscribe({
      next: () => this.load(),
      error: (err: unknown) => {
        this.error = 'Failed to delete book.';
        console.error(err);
      }
    });
  }

  prev(): void {
    this.page = Math.max(1, this.page - 1);
  }

  next(): void {
    this.page = Math.min(this.totalPages, this.page + 1);
  }
}