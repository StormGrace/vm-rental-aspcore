using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using vm_rental.Data.Interface;

namespace vm_rental.ViewModels.CustomValidators
{
  public class UsernameExistsAttribute : ValidationAttribute, IClientModelValidator
  {
    IUserHistoryRepository _userHistoryRepository;

    public UsernameExistsAttribute(IUserHistoryRepository userHistoryRepository)
    {
      _userHistoryRepository = userHistoryRepository;
    }
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
      if (value != null)
      {
        string email = (string)value;

        if (_userHistoryRepository.EmailExists(email))
        {
          return new ValidationResult("Email is already exists.");
        }
      }
      return ValidationResult.Success;
    }

    public void AddValidation(ClientModelValidationContext context)
    {
      if (context == null)
      {
        throw new ArgumentNullException(nameof(context));
      }

      AttributeUtils.MergeAttribute(context.Attributes, "data-val", "true");
      AttributeUtils.MergeAttribute(context.Attributes, "data-val-username", FormatErrorMessage(context.ModelMetadata.GetDisplayName()));
    }
  }

  public class AttributeUtils
  {
    public static bool MergeAttribute(
        IDictionary<string, string> attributes,
        string key,
        string value)
    {
      if (attributes.ContainsKey(key))
      {
        return false;
      }
      attributes.Add(key, value);
      return true;
    }
  }
}
