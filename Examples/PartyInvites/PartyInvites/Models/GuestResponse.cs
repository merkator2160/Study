using System;
using System.ComponentModel.DataAnnotations;

namespace PartyInvites.Models
{
    public class GuestResponse
    {
        [Required(ErrorMessage = "Пожалуйста, введите свое имя")]
        public String Name { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите email")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Вы ввели некорректный email")]
        public String Email { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите телефон")]
        public String Phone { get; set; }

        [Required(ErrorMessage = "Пожалуйста, укажите, примите ли участие в вечеринке")]
        public Boolean? WillAttend { get; set; }
    }
}