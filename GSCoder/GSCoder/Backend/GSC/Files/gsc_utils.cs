namespace GSCoder.Backend
{
    class gsc_utils
    {
        public static string GetModMenuDesign(string code)
        {
            string design_code = "";

            //tokenize the code
            var parsedCode = parser.GetParsedCode(code);

            //get the function InitialisingMenu
            for(int i = 0; i < parsedCode.Count; i++)
            {
                if(parsedCode[i] == "InitialisingMenu" && parsedCode[i + 3] != ";")
                {
                    //get the design code
                    for(int j = i + 3; i < parsedCode.Count; j++)
                    {
                        if(parsedCode[j] == "}")
                        {
                            break;
                        }
                        else
                        {
                            design_code += parsedCode[j];
                        }
                    }
                }
            }

            return design_code;
        }
    }
}