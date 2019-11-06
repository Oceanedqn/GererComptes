namespace WindowsGerer.Bdd
{
    public class SqlConst
    {
        

        public class User
        {
            public const string T_USER = "e_user_eus";
            public const string ID = "EUS_id";
            public const string NAME = "EUS_name";
            public const string FIRSTNAME = "EUS_firstname";
            public const string DATESTART = "EUS_datestart";
            public const string EMAIL = "EUS_email";
            public const string ID_ECI = "ECI_id";
            public const string ID_EMP = "EMP_id";
        }

        public class City
        {
            public const string T_CITY = "e_city_eci";
            public const string ID = "ECI_id";
            public const string CITY = "ECI_city";
            public const string CP = "ECI_CP";
        }

        public class Means
        {
            public const string T_MEANS = "e_meansofpayement_emp";
            public const string ID = "EMP_id";
            public const string MEANS = "EMP_MeansOfPayement";
        }

        public class Payement
        {
            public const string T_PAYEMENT = "e_payement_epa";
            public const string ID = "EPA_id";
            public const string DATE = "EPA_date";
            public const string AMOUNT = "EPA_amount";
            public const string COMMENT = "EPA_comment";
            public const string ID_EUS = "EUS_id";
        }


    }
}
