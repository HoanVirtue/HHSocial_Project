namespace Clone_Main_Project_0710.GenericMethod
{
    public class HandleCharacter
    {
        private static HandleCharacter instance;
        public static HandleCharacter Instance
        {
            get
            {
                if (instance == null)
                    instance = new HandleCharacter();
                return instance;
            }
            private set => instance = value;
        }

        private HandleCharacter()
        {
        }
        
        public bool IsSpecialChar(string characters)
        {
            string specialCharacters = "!@#$%^&*()";
            foreach(char c in characters)
            {
                if(specialCharacters.Contains(c))
                    return true;
            }
            return false;
        }

        public bool IsUpperCaseChar(string characters)
        {
            foreach(char c in characters) {
                if(char.IsUpper(c))
                    return true;
            }
            return false;
        }

        public bool IsLowerCaseChar(string characters)
        {
            foreach(char c in characters) {
                if(char.IsLower(c))
                    return true;
            }
            return false;
        }

        public bool IsDigitChar(string characters)
        {
            foreach(char c in characters) {
                if(char.IsDigit(c))
                    return true;
            }
            return false;
        }

        public bool IsSixChara(string characters)
        {
            if(characters.Length < 6)
                return false;
            return true;
        }
    }
}