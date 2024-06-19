using System.ComponentModel;
using System.Reflection;

namespace C_Utilities
{
    

    public class Enumes
    {
    

public enum CustomerType
    {
             [Description("تمريض")]
        Nursing = 1,

        //[Description(" بون  ")]
        //HospitalManagement = 2,

        [Description("عمليات")]
        oprations = 3,

        [Description("عمال")]
        Worker = 4,

        [Description("حالات  ")]
        OtherPeople = 5
    }






    public enum Gender
        {
            [Description("ذكر  ")]
            Male,
            [Description("انثى  ")]

            Female,
        }

        public enum FinancialAdvanceType
        {
            BuyProduct,
            GetMoney
        }

        public enum PaymentStatus
        {
            [Description("تم الدفع")]
            Paid = 1,

            [Description("  لم يتم الدفع")]
            NotPaid = 2,

            [Description(" دفع جزء من المبلغ  ")]
            PartialPaid = 3
        }

        public enum Status
        {
            New,
            InProgress,
            Completed,
            Failed
        }

        public enum CategoryType
        {
           
            [Description(" مشروب بارد  ")]
            ColdDrink=1,
            [Description("  مشروب ساخن")]
            HotDrink=2,
            [Description("اضافات")]
            Addon=3,

        } 
        
        public enum HospitalOroprationtyp
        {
           
            [Description("اضافه على المستشفى  ")]
            Hospital = 1,
            [Description("  اضافه على العمليات")]
            oprationtyp = 2,
           

        }




    }
}




public static class EnumExtensions
{
    public static string GetDescription(this Enum value)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        var field = value.GetType().GetField(value.ToString());

        if (field == null)
        {
            throw new ArgumentException($"Invalid enum value '{value}' for enum type '{value.GetType()}'");
        }

        var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
        return attribute == null ? value.ToString() : attribute.Description;
    }
}

