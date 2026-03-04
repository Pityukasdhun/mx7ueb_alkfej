import { Routes } from '@angular/router';
import { BooksComponent } from './pages/books/books.component';
import { UsersComponent } from './pages/users/users.component';

export const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'books' },
  { path: 'books', component: BooksComponent },
  { path: 'users', component: UsersComponent },
  { path: '**', redirectTo: 'books' }
];