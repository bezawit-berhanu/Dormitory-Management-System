export enum StudentStatus {
  Active = 1,
  Inactive = 2,
  Graduated = 3,
  Suspended = 4,
}

export interface Student {
  studentId: string;
  name: string;
  departmentName: string;
  gender: string;
  dateOfBirth: string;
  emergencyContactNumber: string;
  yearOfStudy: number;
  status: StudentStatus;
}