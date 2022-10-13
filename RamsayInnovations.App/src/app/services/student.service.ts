import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Student } from '../models/student';
import { HttpClient } from '@angular/common/http';


const baseUrl = 'https://localhost:44354/api/v1/Students';


@Injectable({
  providedIn: 'root'
})
export class StudentService {

  constructor(private http: HttpClient) { }

  public GetStudents(): Observable<Student[]> {
    return this.http.get<Student[]>(baseUrl);
  }

  public GetStudent(id: number): Observable<Student> {
    return this.http.get<Student>(`${baseUrl}/${id}`);
  }

  public DeleteStudent(id: number): Observable<DeleteStudentResponse> {
    return this.http.delete<DeleteStudentResponse>(`${baseUrl}/${id}`);
  }

  public AddStudent(request: AddStudentRequest): Observable<AddStudentResponse> {
 
    return this.http.post<AddStudentResponse>(baseUrl, request);
  }

  public UpdateStudent(id: number, request: UpdateStudentRequest): Observable<UpdateStudentResponse> {
    return this.http.put<UpdateStudentResponse>(`${baseUrl}/${id}`, request);
  }



}

export class AddStudentRequest {
  public username: string;
  public firstName: string;
  public lastName: string;
  public age: number;
  public career: string;
}

export class AddStudentResponse {
  public student: Student
  public mensaje: string
  public isValid: boolean
}

export class UpdateStudentRequest {
  public id: number;
  public username: string;
  public firstName: string;
  public lastName: string;
  public age: number;
  public career: string;
}

export class UpdateStudentResponse {
  public student: Student
  public mensaje: string
  public isValid: boolean
}

export class DeleteStudentResponse {
  public mensaje: string
  public isValid: boolean
}