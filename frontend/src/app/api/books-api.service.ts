import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Book {
  id?: string;
  title: string;
  author: string;
  year: number;
}

export interface CreateBookRequest {
  title: string;
  author: string;
  year: number;
}

@Injectable({ providedIn: 'root' })
export class BooksApiService {
  private readonly baseUrl = 'http://localhost:5146';

  constructor(private http: HttpClient) {}

  list(q?: string): Observable<Book[]> {
    let params = new HttpParams();
    if (q && q.trim().length > 0) params = params.set('q', q.trim());
    return this.http.get<Book[]>(`${this.baseUrl}/api/mcp/Books`, { params });
  }

  create(req: CreateBookRequest): Observable<Book> {
    return this.http.post<Book>(`${this.baseUrl}/api/mcp/Books`, req);
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/api/mcp/Books/${id}`);
  }
}