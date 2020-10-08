using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using MailSender.Models.Base;

namespace WpfMailSender.Models
{
    public class Recipient : Person, IDataErrorInfo
    {
        public override string Name
        {
            get => base.Name;
            set
            {
                //if (value is null)
                //    throw new ArgumentNullException(nameof(value));

                //if (value == "")
                //    throw new ArgumentException("Имя не может быть пустой строкой", nameof(value));

                if (value == "666")
                    throw new ArgumentException("Запрещено вводить 666!", nameof(value));


                base.Name = value;
            }
        }
        string IDataErrorInfo.Error { get; } = null;

        string IDataErrorInfo.this[string PropertyName]
        {
            get
            {
                switch (PropertyName)
                {
                    default: return null;

                    case nameof(Name):
                        var name = Name;
                        if (name is null) return "Введите имя";
                        if (name.Length < 2) return "Слишком короткое имя";
                        if (name.Length > 20) return "Слишком длинное имя";

                        return null;

                    case nameof(Address):
                        return null;
                }
            }
        }
    }
}
