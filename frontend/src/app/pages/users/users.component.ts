import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

interface User {
  id?: string;
  displayName: string;
  email: string;
}

@Component({
  selector: 'app-users',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './users.component.html'
})
export class UsersComponent {

  users: User[] = [];

  displayName = '';
  email = '';

  loading = false;
  error: string | null = null;

  api = 'http://localhost:5232/api/users';

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.load();
  }

  load() {
    this.loading = true;

    this.http.get<User[]>(this.api).subscribe({
      next: data => {
        this.users = data ?? [];
        this.loading = false;
      },
      error: err => {
        this.error = 'Failed to load users';
        this.loading = false;
        console.error(err);
      }
    });
  }

  create() {

    if (!this.displayName.trim() || !this.email.trim()) {
      this.error = 'Display name and email required';
      return;
    }

    this.http.post<User>(this.api, {
      displayName: this.displayName.trim(),
      email: this.email.trim()
    }).subscribe({
      next: () => {
        this.displayName = '';
        this.email = '';
        this.load();
      },
      error: err => {
        this.error = 'Failed to create user';
        console.error(err);
      }
    });
  }
}