namespace bies
{
    public class Enums
    {
        public enum Tabs : byte
        {
            trades0 = 0,
            tenderDocs1 = 1,
            invite2 = 2,
            openprotocol3 = 3,
            ratingprotokol4 = 4,
            contract5 = 5,
            users6 = 6,
            payment7 = 7,
            finance8 = 8
        }

        public enum group : byte
        {
            admin = 1,
            boss = 2,
            user = 3,
            buh = 4
        }

        public enum tradetype : byte
        {
            withPredqulifications = 1,
            withoutPredqulifications = 2
        }

        public enum typedoc : byte
        {
            tenderDocs = 0,
            invite = 1,
            openprotocol = 2,
            ratingreport = 3,
            contract = 4,
            contractdoc = 5,
            contractworksdoc = 6,
            template = 7
        }

        public enum invitestatus : byte
        {
            site = 0,
            newspaper = 1,
            sendingemail = 2
        }

        public enum currencyID : byte
        {
            byr = 1,
            usd = 2,
            eur = 3,
            rub = 4
        }

        public enum typefundingID : byte
        {
            sredstva_rb = 1,
            sredstva_vbrr = 2
        }

        public enum actstartID : byte
        {
            yes = 1,
            no = 2,
            pereoformlenie = 3
        }

        public enum cataqlogPaymentID : byte
        {
            pZaim = 1,
            pObject = 2,
            pContragent = 3,
            pContract = 4,
            pCategorycontract = 5,
            pCategorywork = 6,
            pSubcategorywork = 7,
            pOblast = 8
        }

        public enum pTypePaymentID : byte
        {
            fact = 1,
            plan = 2,
            finance_direct = 3,
            finance_specaccount = 4
            
            
        }
        

    }
}
