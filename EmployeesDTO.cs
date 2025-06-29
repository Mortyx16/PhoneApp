using System;
using System.Collections.Generic;
using System.Linq;

public class EmployeesDTO : DataTransferObject
{
    public string Name { get; set; }

    public string Phone
    {
        get { return _phones.Any() ? _phones.LastOrDefault().Value : "-"; }
    }

    public void AddPhone(string phone)
    {
        if (string.IsNullOrEmpty(phone))
        {
            throw new Exception("Необходимо предоставить телефон!");
        }

        _phones.Add(DateTime.Now, phone);
    }

    private readonly Dictionary<DateTime, string> _phones = new Dictionary<DateTime, string>();
}
