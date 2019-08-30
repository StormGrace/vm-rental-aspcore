using System;

namespace vm_rental.Utility.Extensions
{
  class SubjectNameAttribute : Attribute
  {
    public SubjectNameAttribute() { }

    public SubjectNameAttribute(string subjectName)
    {
      SubjectName = subjectName;
    }

    public string SubjectName {get; set;}
  }
}