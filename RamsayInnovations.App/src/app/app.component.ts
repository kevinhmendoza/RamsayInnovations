import { Component, OnInit } from '@angular/core';
import { Student } from './models/student';
import { AddStudentRequest, StudentService, UpdateStudentRequest } from './services/student.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  students: Student[];
  studentForm: boolean;
  isNewStudent: boolean;
  newStudent: any = {};
  editStudentForm: boolean;
  editedStudent: any = {};

  constructor(private studentService: StudentService, private toastrService: ToastrService) { }

  ngOnInit() {
    this.getStudents();
  }

  getStudents() {
    this.studentService.GetStudents().subscribe(response => {
      this.students = response;
    }, err => {
    });;

  }

  showEditStudentForm(student: Student) {
    if (!student) {
      this.studentForm = false;
      return;
    }
    this.editStudentForm = true;
    this.editedStudent = student;
  }

  showAddStudentForm() {
    // resets form if edited student
    if (this.students.length) {
      this.newStudent = {};
    }
    this.studentForm = true;
    this.isNewStudent = true;

  }

  saveStudent(student: Student) {

    if (this.isNewStudent) {
      // add a new student
      let request: AddStudentRequest = {
        username: student.username,
        age: student.age,
        firstName: student.firstName,
        lastName: student.lastName,
        career: student.career
      };
      this.studentService.AddStudent(request).subscribe(response => {
        console.log(response);
        this.toastrService.success('Add Student', response.mensaje);
        this.getStudents();
      }, err => {
      });
    }
    this.studentForm = false;
  }

  updateStudent() {


    let request: UpdateStudentRequest = {
      id: this.editedStudent.id,
      username: this.editedStudent.username,
      age: this.editedStudent.age,
      firstName: this.editedStudent.firstName,
      lastName: this.editedStudent.lastName,
      career: this.editedStudent.career
    };

    this.studentService.UpdateStudent(request.id, request).subscribe(response => {
      console.log(response);
      this.editStudentForm = false;
      this.editedStudent = {};
      this.getStudents();
    }, err => {
    });;

  }

  removeStudent(student: Student) {
    this.studentService.DeleteStudent(student.id).subscribe(response => {
      this.getStudents();
      console.log(response);
    }, err => {

    });
  }

  cancelEdits() {
    this.editedStudent = {};
    this.editStudentForm = false;
  }

  cancelNewStudent() {
    this.newStudent = {};
    this.studentForm = false;
  }

}
