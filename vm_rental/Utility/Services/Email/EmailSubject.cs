﻿using System;
 
namespace vm_rental.Utility.Services.Email
{
  public enum EmailSubject
  {
    [SubjectName("Please confirm your email")]
    EmailConfirmationSubject,
    [SubjectName("Password change request")]
    PasswordChangeSubject,
    [SubjectName("Password reset request")]
    PasswordForgottenSubject
  }

  public class EmailSubjectType
  {
     public EmailSubject SubjectType { get; private set; }

     public EmailSubjectType(EmailSubject subject)
     {
        SubjectType = subject;
     }

     public void GetSubjectType()
     {
        SubjectType.GetType();
     }
  }

  class SubjectNameAttribute : Attribute
  {
    public SubjectNameAttribute() { }

    public SubjectNameAttribute(string subjectName)
    {
      SubjectName = subjectName;
    }

    public string SubjectName { get; set; }
  }

  public static class EnumExtensions
  {
    public static string GetSubjectName(this EmailSubject val)
    {
      SubjectNameAttribute[] attributes = (SubjectNameAttribute[])val
       .GetType()
       .GetField(val.ToString())
       .GetCustomAttributes(typeof(SubjectNameAttribute), false);

      return attributes.Length > 0 ? attributes[0].SubjectName : string.Empty;
    }
  }
}
