using System.Collections.Generic;
using College.Model.DataTransferObject.StudentSayStudents;
using College.Model.DataTransferObject.StudentsSayDto;

namespace College.Model.DataTransferObject.OtherDto
{
    public class StudentSaysStudentsModelDto
    {
        public StudentSayModelDto SayModel { get; set; }
        public IList<StudentSayStudentsModelDto> StudentsModel { get; set; }
    }
}