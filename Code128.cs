using System;
using System;
using System.Collections.Generic;
using System.Text;

namespace twCode128Generator
{

    //===>  Encode method based on C# code from http://grandzebu.net/informatique/codbar-en/code128.htm
    // -- with changes

    ///////*
    ////// *	Auteur	:	Joffrey VERDIER
    ////// *	Date	:	08/2006
    ////// *	Légal	:	OpenSource © 2007 AVRANCHES
    ////// * 
    ////// */


    public class Code128
    {
        private Dictionary<char, string> UglyEncoder = null;

        public Code128() { }
        /// <summary>
        /// Encode la chaine en code128
        /// </summary>
        /// <param name="chaine">Chaine à transcrire</param>
        /// <returns></returns>
        public String Encode(String chaine)
        {
            int ind = 1;
            int checksum = 0;
            int mini;
            int dummy;
            bool tableB;
            String code128;
            int longueur;

            code128 = "";
            longueur = chaine.Length;


            if (longueur == 0)
            {
                Console.WriteLine("\n chaine vide");
            }
            else
            {
                for (ind = 0; ind < longueur; ind++)
                {
                    if ((chaine[ind] < 32) || (chaine[ind] > 126))
                    {
                        Console.WriteLine("\n chaine invalide");
                    }
                }
            }


            tableB = true;
            ind = 0;



            while (ind < longueur)
            {

                if (tableB == true)
                {
                    if ((ind == 0) || (ind + 3 == longueur - 1))
                    {
                        mini = 4;
                    }
                    else
                    {
                        mini = 6;
                    }

                    mini = mini - 1;

                    if ((ind + mini) <= longueur - 1)
                    {
                        while (mini >= 0)
                        {
                            if ((chaine[ind + mini] < 48) || (chaine[ind + mini] > 57))
                            {
                                //Console.WriteLine("\n exit");
                                break;
                            }
                            mini = mini - 1;
                        }
                    }


                    if (mini < 0)
                    {
                        if (ind == 0)
                        {
                            code128 = Char.ToString((char)205);

                        }
                        else
                        {
                            code128 = code128 + Char.ToString((char)199);

                        }
                        tableB = false;
                    }
                    else
                    {

                        if (ind == 0)
                        {
                            code128 = Char.ToString((char)204);
                        }
                    }
                }

                if (tableB == false)
                {
                    mini = 2;
                    mini = mini - 1;
                    if (ind + mini < longueur)
                    {
                        while (mini >= 0)
                        {

                            if (((chaine[ind + mini]) < 48) || ((chaine[ind]) > 57))
                            {
                                break;
                            }
                            mini = mini - 1;
                        }
                    }
                    if (mini < 0)
                    {
                        dummy = Int32.Parse(chaine.Substring(ind, 2));

                        //Console.WriteLine("\n  dummy ici : " + dummy);

                        if (dummy < 95)
                        {
                            dummy = dummy + 32;
                        }
                        else
                        {
                            dummy = dummy + 100;
                        }
                        code128 = code128 + (char)(dummy);

                        ind = ind + 2;
                    }
                    else
                    {
                        code128 = code128 + Char.ToString((char)200);
                        tableB = true;
                    }
                }
                if (tableB == true)
                {

                    code128 = code128 + chaine[ind];
                    ind = ind + 1;
                }
            }

            for (ind = 0; ind <= code128.Length - 1; ind++)
            {
                dummy = code128[ind];
                //Console.WriteLine("\n  et voila dummy : " + dummy);
                if (dummy < 127)
                {
                    dummy = dummy - 32;
                }
                else
                {
                    dummy = dummy - 100;
                }
                if (ind == 0)
                {
                    checksum = dummy;
                }
                checksum = (checksum + (ind) * dummy) % 103;
            }

            if (checksum < 95)
            {
                checksum = checksum + 32;
            }
            else
            {
                checksum = checksum + 100;
            }
            code128 = code128 + Char.ToString((char)checksum)
                      + Char.ToString((char)206);

            return code128;
        }

        public string UglyEncode(string strIn)
        {
            if (UglyEncoder == null) { UglyEncoderLoad(); }

            StringBuilder sbOut = new StringBuilder();


            foreach (char c in strIn)
            {
                string encoded = "___";
                if (UglyEncoder.ContainsKey(c))
                {
                    if (c == 'V') { int x = 1; }
                    encoded = UglyEncoder[c];
                }
                sbOut.Append(encoded);
            }

            sbOut.Append("1");

            string strOut = sbOut.ToString();
            return strOut;
        }

        private void UglyEncoderLoad()
        {
            UglyEncoder = new Dictionary<char, string>();

            UglyEncoder.Add((char)32, "%&&");           /*   */
            UglyEncoder.Add((char)33, "&%&");           /* ! */
            UglyEncoder.Add((char)34, "&&%");           /* " */
            UglyEncoder.Add((char)35, "\"\"'");         /* # */
            UglyEncoder.Add((char)36, "\"#&");          /* $ */
            UglyEncoder.Add((char)37, "#\"&");          /* % */
            UglyEncoder.Add((char)38, "\"&#");          /* & */
            UglyEncoder.Add((char)39, "\"'\"");         /* ' */
            UglyEncoder.Add((char)40, "#&\"");          /* ( */
            UglyEncoder.Add((char)41, "&\"#");          /* ) */
            UglyEncoder.Add((char)42, "&#\"");          /* * */
            UglyEncoder.Add((char)43, "'\"\"");         /* + */
            UglyEncoder.Add((char)44, "!&*");           /* , */
            UglyEncoder.Add((char)45, "\"%*");          /* - */
            UglyEncoder.Add((char)46, "\"&)");          /* . */
            UglyEncoder.Add((char)47, "!*&");           /* / */
            UglyEncoder.Add((char)48, "\")&");          /* 0 */
            UglyEncoder.Add((char)49, "\"*%");          /* 1 */
            UglyEncoder.Add((char)50, "&*!");           /* 2 */
            UglyEncoder.Add((char)51, "&!*");           /* 3 */
            UglyEncoder.Add((char)52, "&\")");          /* 4 */
            UglyEncoder.Add((char)53, "%*\"");          /* 5 */
            UglyEncoder.Add((char)54, "&)\"");          /* 6 */
            UglyEncoder.Add((char)55, ")%)");           /* 7 */
            UglyEncoder.Add((char)56, ")\"&");          /* 8 */
            UglyEncoder.Add((char)57, "*!&");           /* 9 */
            UglyEncoder.Add((char)58, "*\"%");          /* : */
            UglyEncoder.Add((char)59, ")&\"");          /* ; */
            UglyEncoder.Add((char)60, "*%\"");          /* < */
            UglyEncoder.Add((char)61, "*&!");           /* = */
            UglyEncoder.Add((char)62, "%%'");           /* > */
            UglyEncoder.Add((char)63, "%'%");           /* ? */
            UglyEncoder.Add((char)64, "'%%");           /* @ */
            UglyEncoder.Add((char)65, "!#'");           /* A */
            UglyEncoder.Add((char)66, "#!'");           /* B */
            UglyEncoder.Add((char)67, "##%");           /* C */
            UglyEncoder.Add((char)68, "!'#");           /* D */
            UglyEncoder.Add((char)69, "#%#");           /* E */
            UglyEncoder.Add((char)70, "#'!");           /* F */
            UglyEncoder.Add((char)71, "%##");           /* G */
            UglyEncoder.Add((char)72, "'!#");           /* H */
            UglyEncoder.Add((char)73, "'#!");           /* I */
            UglyEncoder.Add((char)74, "!%+");           /* J */
            UglyEncoder.Add((char)75, "!')");           /* K */
            UglyEncoder.Add((char)76, "#%)");           /* L */
            UglyEncoder.Add((char)77, "!)'");           /* M */
            UglyEncoder.Add((char)78, "!+%");           /* N */
            UglyEncoder.Add((char)79, "#)%");           /* O */
            UglyEncoder.Add((char)80, "))%");           /* P */
            UglyEncoder.Add((char)81, "%#)");           /* Q */
            UglyEncoder.Add((char)82, "'!)");           /* R */
            UglyEncoder.Add((char)83, "%)#");           /* S */
            UglyEncoder.Add((char)84, "%+!");           /* T */
            UglyEncoder.Add((char)85, "%))");           /* U */
            UglyEncoder.Add((char)86, ")!'");           /* V */
            UglyEncoder.Add((char)87, ")#%");           /* W */
            UglyEncoder.Add((char)88, "+!%");           /* X */
            UglyEncoder.Add((char)89, ")%#");           /* Y */
            UglyEncoder.Add((char)90, ")'!");           /* Z */
            UglyEncoder.Add((char)91, "+%!");           /* [ */
            UglyEncoder.Add((char)92, ")-!");           /* \ */
            UglyEncoder.Add((char)93, "&$!");           /* ] */
            UglyEncoder.Add((char)94, "/!!");           /* ^ */
            UglyEncoder.Add((char)95, "!\"(");          /* _ */
            UglyEncoder.Add((char)96, "!$&");           /* ` */
            UglyEncoder.Add((char)97, "\"!(");          /* a */
            UglyEncoder.Add((char)98, "\"$%");          /* b */
            UglyEncoder.Add((char)99, "$!&");           /* c */
            UglyEncoder.Add((char)100, "$\"%");         /* d */
            UglyEncoder.Add((char)101, "!&$");          /* e */
            UglyEncoder.Add((char)102, "!(\"");         /* f */
            UglyEncoder.Add((char)103, "\"%$");         /* g */
            UglyEncoder.Add((char)104, "\"(!");         /* h */
            UglyEncoder.Add((char)105, "$%\"");         /* i */
            UglyEncoder.Add((char)106, "$&!");          /* j */
            UglyEncoder.Add((char)107, "(\"!");         /* k */
            UglyEncoder.Add((char)108, "&!$");          /* l */
            UglyEncoder.Add((char)109, "-)!");          /* m */
            UglyEncoder.Add((char)110, "(!\"");         /* n */
            UglyEncoder.Add((char)111, "#-!");          /* o */
            UglyEncoder.Add((char)112, "!\".");         /* p */
            UglyEncoder.Add((char)113, "\"!.");         /* q */
            UglyEncoder.Add((char)114, "\"\"-");        /* r */
            UglyEncoder.Add((char)115, "!.\"");         /* s */
            UglyEncoder.Add((char)116, "\"-\"");        /* t */
            UglyEncoder.Add((char)117, "\".!");         /* u */
            UglyEncoder.Add((char)118, "-\"\"");        /* v */
            UglyEncoder.Add((char)119, ".!\"");         /* w */
            UglyEncoder.Add((char)120, ".\"!");         /* x */
            UglyEncoder.Add((char)121, "%%-");          /* y */
            UglyEncoder.Add((char)122, "%-%");          /* z */
            UglyEncoder.Add((char)123, "-%%");          /* { */
            UglyEncoder.Add((char)124, "!!/");          /* | */
            UglyEncoder.Add((char)125, "!#-");          /* } */
            UglyEncoder.Add((char)126, "#!-");          /* ~ */
            UglyEncoder.Add((char)195, "!-#");          /* A */
            UglyEncoder.Add((char)196, "!/!");          /* Ä */
            UglyEncoder.Add((char)197, "-!#");          /* Å */
            UglyEncoder.Add((char)198, "-#!");          /* Æ */
            UglyEncoder.Add((char)199, "!)-");          /* Ç */
            UglyEncoder.Add((char)200, "!-)");          /* E */
            UglyEncoder.Add((char)201, ")!-");          /* É */
            UglyEncoder.Add((char)202, "-!)");          /* E */
            UglyEncoder.Add((char)204, "%\"$");         /* I */
            UglyEncoder.Add((char)206, "')!");          /* I */

        }

        private void UglyEncoderLoad_OLD()
        {
            UglyEncoder = new Dictionary<char, string>();
            //UglyEncoder.Add(' ', "___");
            //UglyEncoder.Add(' ', "___");
            //UglyEncoder.Add(' ', "___");
            //UglyEncoder.Add(' ', "___");
            //UglyEncoder.Add(' ', "___");
            //UglyEncoder.Add(' ', "___");
            //UglyEncoder.Add(' ', "___");
            //UglyEncoder.Add(' ', "___");
            //UglyEncoder.Add(' ', "___");
            //UglyEncoder.Add(' ', "___");
            //UglyEncoder.Add(' ', "___");
            //UglyEncoder.Add(' ', "___");
            //UglyEncoder.Add(' ', "___");
            //UglyEncoder.Add(' ', "___");
            //UglyEncoder.Add(' ', "___");
            //UglyEncoder.Add(' ', "___");
            //UglyEncoder.Add(' ', "___");
            //UglyEncoder.Add(' ', "___");
            //UglyEncoder.Add(' ', "___");
            //UglyEncoder.Add(' ', "___");
            //UglyEncoder.Add(' ', "___");

            UglyEncoder.Add(' ', "%&&");
            UglyEncoder.Add('!', "&%&");
            UglyEncoder.Add('#', "\"\"'");
            UglyEncoder.Add('$', "\"#&");
            UglyEncoder.Add('%', "#\"&");
            UglyEncoder.Add('&', "\"&#");
            UglyEncoder.Add('(', "#&\"");
            UglyEncoder.Add(')', "&\"#");
            UglyEncoder.Add('*', "&#\"");
            UglyEncoder.Add('+', "'\"\"");
            UglyEncoder.Add(',', "!&*");
            UglyEncoder.Add('-', "\"%*");
            UglyEncoder.Add('.', "\"&)");
            UglyEncoder.Add('/', "!*&");
            UglyEncoder.Add('0', "\")&");
            UglyEncoder.Add('1', "\"*%");
            UglyEncoder.Add('2', "&*!");    // <== Problem dual
            UglyEncoder.Add('3', "&!*");
            UglyEncoder.Add('4', "&\")");
            UglyEncoder.Add('5', "%*\"");
            UglyEncoder.Add('6', "&)\"");
            UglyEncoder.Add('7', ")%)");
            //UglyEncoder.Add('8', "-!#");
            UglyEncoder.Add('8', ")\"&");
            UglyEncoder.Add('9', "*!&");
            UglyEncoder.Add(':', "*\"%");
            UglyEncoder.Add(';', ")&\"");
            UglyEncoder.Add('<', "*%\"");
            UglyEncoder.Add('=', "*&!");
            UglyEncoder.Add('>', "%%'");
            UglyEncoder.Add('?', "%'%");
            UglyEncoder.Add('@', "'%%");
            UglyEncoder.Add('A', "!#'");
            UglyEncoder.Add('B', "#!'");
            UglyEncoder.Add('C', "##%");
            UglyEncoder.Add('D', "!'#");
            UglyEncoder.Add('E', "#%#");    // <== Problem dual
            UglyEncoder.Add('F', "#'!");
            UglyEncoder.Add('G', "%##");
            UglyEncoder.Add('H', "'!#");
            UglyEncoder.Add('I', "'#!");
            UglyEncoder.Add('J', "!%+");
            UglyEncoder.Add('K', "!')");
            UglyEncoder.Add('L', "#%)");
            UglyEncoder.Add('M', "!)'");
            UglyEncoder.Add('N', "!+%");
            UglyEncoder.Add('O', "&*!");    // <== Problem dual
            UglyEncoder.Add('P', "))%");
            UglyEncoder.Add('Q', "%#)");
            UglyEncoder.Add('R', "'!)");
            UglyEncoder.Add('S', "%)#");
            UglyEncoder.Add('T', "%+!");
            UglyEncoder.Add('U', "%))");
            UglyEncoder.Add('V', ")!'");
            UglyEncoder.Add('W', ")#%");
            UglyEncoder.Add('X', "+!%");
            UglyEncoder.Add('Y', ")%#");
            UglyEncoder.Add('Z', ")'!");
            UglyEncoder.Add('[', "+%!");
            UglyEncoder.Add('\"', "&&%");
            UglyEncoder.Add('\'', "\"'\"");
            UglyEncoder.Add('\\', ")-!");
            UglyEncoder.Add(']', "&$!");
            UglyEncoder.Add('^', "/!!");
            UglyEncoder.Add('_', "!\"(");
            UglyEncoder.Add('`', "!$&");
            UglyEncoder.Add('a', "\"!(");
            UglyEncoder.Add('b', "\"$%");
            UglyEncoder.Add('c', "$!&");
            UglyEncoder.Add('d', "$\"%");
            UglyEncoder.Add('e', "!&$");
            UglyEncoder.Add('f', "!(\"");
            UglyEncoder.Add('g', "\"%$");
            //UglyEncoder.Add('h', "-!'");
            UglyEncoder.Add('h', "\"(!");
            UglyEncoder.Add('i', "$%\"");
            UglyEncoder.Add('j', "$&!");
            UglyEncoder.Add('k', "(\"!");
            UglyEncoder.Add('l', "&!$");
            UglyEncoder.Add('m', "-)!");
            UglyEncoder.Add('n', "(!\"");
            UglyEncoder.Add('o', "#-!");
            UglyEncoder.Add('p', "!\".");
            UglyEncoder.Add('q', "\"!.");
            UglyEncoder.Add('r', "\"\"-");
            UglyEncoder.Add('s', "!.\"");
            UglyEncoder.Add('t', "\"-\"");
            UglyEncoder.Add('u', "\".!");
            UglyEncoder.Add('v', "-\"\"");
            UglyEncoder.Add('w', ".!\"");
            UglyEncoder.Add('x', ".\"!");
            UglyEncoder.Add('y', "%%-");
            UglyEncoder.Add('z', "%-%");
            UglyEncoder.Add('{', "-%%");
            UglyEncoder.Add('|', "!!/");
            UglyEncoder.Add('}', "!#-");
            UglyEncoder.Add('~', "#!-");
            UglyEncoder.Add('Â', "%&&");
            UglyEncoder.Add('Ã', "!-#");
            UglyEncoder.Add('Ä', "!/!");
            UglyEncoder.Add('Å', "-!#");
            UglyEncoder.Add('Æ', "-#!");
            UglyEncoder.Add('Ç', "!)-");
            UglyEncoder.Add('É', ")!-");
            UglyEncoder.Add('Ì', "%\"$");
            UglyEncoder.Add('Î', "')!");
            ////////////////////--New
            //////////////////UglyEncoder.Add('Î', "')!");
            //////////////////UglyEncoder.Add('2', ")\"&");
            //////////////////UglyEncoder.Add('L', "#%)");
            //////////////////UglyEncoder.Add('S', "%)#");
            //////////////////UglyEncoder.Add(')', "&\"#");
            //////////////////UglyEncoder.Add('Q', "%#)");
            //////////////////UglyEncoder.Add('1', "\"*%");
            //////////////////UglyEncoder.Add('T', "%+!");
            //////////////////UglyEncoder.Add(':', "*\"%");
            //////////////////UglyEncoder.Add('q', "\"!.");
            //////////////////UglyEncoder.Add('\\', ")-!");
            //////////////////UglyEncoder.Add('h', "-!'");
            //////////////////UglyEncoder.Add('=', "*&!");
            //////////////////UglyEncoder.Add('[', "+%!");
            //////////////////UglyEncoder.Add('&', "\"&#");
            //////////////////UglyEncoder.Add('Z', ")'!");
            //////////////////UglyEncoder.Add('8', "-!#");
            //////////////////UglyEncoder.Add('G', "%##");
            //////////////////UglyEncoder.Add('(', "#&\"");
            //////////////////UglyEncoder.Add('/', "!*&");
            //////////////////UglyEncoder.Add('Å', "-!#");
            //////////////////UglyEncoder.Add('`', "!$&");
            //////////////////UglyEncoder.Add('%', "#\"&");
            //////////////////UglyEncoder.Add('\"', "&&%");
            //////////////////UglyEncoder.Add('~', "#!-");
            //////////////////UglyEncoder.Add('^', "/!!");
            //////////////////UglyEncoder.Add('J', "!%+");
            //////////////////UglyEncoder.Add('C', "##%");
            //////////////////UglyEncoder.Add('o', "#-!");
            //////////////////UglyEncoder.Add('k', "(\"!");
            //////////////////UglyEncoder.Add('g', "\"%$");
            //////////////////UglyEncoder.Add('6', "&)\"");
            //////////////////UglyEncoder.Add(';', ")&\"");
            //////////////////UglyEncoder.Add('s', "!.\"");
            //////////////////UglyEncoder.Add('?', "%'%");
            //////////////////////UglyEncoder.Add('6', "&)\"");
            //////////////////UglyEncoder.Add('+', "'\"\"");
            //////////////////UglyEncoder.Add('!', "&%&");
            //////////////////UglyEncoder.Add('>', "%%'");
            //////////////////UglyEncoder.Add('}', "!#-");
            //////////////////UglyEncoder.Add('*', "&#\"");
            //////////////////UglyEncoder.Add('Æ', "-#!");
            //////////////////UglyEncoder.Add('9', "*!&");
            //////////////////UglyEncoder.Add('r', "\"\"-");
            //////////////////UglyEncoder.Add('m', "-)!");
            //////////////////UglyEncoder.Add(',', "!&*");
            //////////////////UglyEncoder.Add('@', "'%%");
            //////////////////////UglyEncoder.Add('f', "-!)");
            //////////////////UglyEncoder.Add('l', "&!$");
            //////////////////////UglyEncoder.Add('O', "#)%");
            //////////////////UglyEncoder.Add('O', "&*!");
            //////////////////UglyEncoder.Add('H', "'!#");
            //////////////////UglyEncoder.Add('K', "!')");
            //////////////////UglyEncoder.Add('D', "!'#");
            //////////////////UglyEncoder.Add('{', "-%%");
            //////////////////UglyEncoder.Add('d', "$\"%");
            //////////////////UglyEncoder.Add('I', "'#!");
            //////////////////////UglyEncoder.Add('E', "-!)");
            //////////////////UglyEncoder.Add('R', "'!)");
            //////////////////UglyEncoder.Add(']', "&$!");
            //////////////////UglyEncoder.Add('N', "!+%");
            //////////////////UglyEncoder.Add('F', "#'!");
            //////////////////UglyEncoder.Add('\'', "\"'\"");
            //////////////////UglyEncoder.Add('_', "!\"(");
            ////////////////////UglyEncoder.Add(' ', "___");
            ////////////////////UglyEncoder.Add(' ', "___");
            ////////////////////UglyEncoder.Add(' ', "___");
            ////////////////////UglyEncoder.Add(' ', "___");
            ////////////////////UglyEncoder.Add(' ', "___");
            ////////////////////UglyEncoder.Add(' ', "___");
            ////////////////////UglyEncoder.Add(' ', "___");
            ////////////////////UglyEncoder.Add(' ', "___");
            ////////////////////UglyEncoder.Add(' ', "___");
            ////////////////////UglyEncoder.Add(' ', "___");
            ////////////////////UglyEncoder.Add(' ', "___");
            ////////////////////UglyEncoder.Add(' ', "___");
            ////////////////////UglyEncoder.Add(' ', "___");
            ////////////////////UglyEncoder.Add(' ', "___");
            ////////////////////UglyEncoder.Add(' ', "___");
            ////////////////////UglyEncoder.Add(' ', "___");
            ////////////////////UglyEncoder.Add(' ', "___");
            ////////////////////UglyEncoder.Add(' ', "___");
            ////////////////////UglyEncoder.Add(' ', "___");
            ////////////////////UglyEncoder.Add(' ', "___");
            ////////////////////UglyEncoder.Add(' ', "___");
            ////////////////////--

            //////////////////UglyEncoder.Add(' ', "%&&");
            //////////////////UglyEncoder.Add('-', "\"%*");
            //////////////////UglyEncoder.Add('#', "\"\"'");
            //////////////////UglyEncoder.Add('$', "\"#&");
            //////////////////UglyEncoder.Add('.', "\"&)");
            //////////////////UglyEncoder.Add('|', "!!/");
            //////////////////UglyEncoder.Add('<', "*%\"");
            //////////////////UglyEncoder.Add('0', "\")&");
            //////////////////UglyEncoder.Add('3', "&!*");
            //////////////////UglyEncoder.Add('4', "&\")");
            //////////////////UglyEncoder.Add('5', "%*\"");
            //////////////////UglyEncoder.Add('7', ")%)");
            ////////////////////UglyEncoder.Add('A', "\"#&");
            //////////////////UglyEncoder.Add('A', "!#'");
            //////////////////UglyEncoder.Add('a', "\"!(");
            //////////////////UglyEncoder.Add('Â', "%&&");
            //////////////////UglyEncoder.Add('Ä', "!/!");
            //////////////////UglyEncoder.Add('Ã', "!-#");
            //////////////////UglyEncoder.Add('B', "#!'");
            //////////////////UglyEncoder.Add('b', "\"$%");
            //////////////////UglyEncoder.Add('c', "$!&");
            //////////////////UglyEncoder.Add('Ç', "!)-");
            //////////////////UglyEncoder.Add('e', "!&$");
            //////////////////UglyEncoder.Add('É', ")!-");
            //////////////////UglyEncoder.Add('i', "$%\"");
            //////////////////UglyEncoder.Add('Ì', "%\"$");
            //////////////////UglyEncoder.Add('j', "$&!");
            //////////////////UglyEncoder.Add('M', "!)'");
            //////////////////UglyEncoder.Add('n', "(!\"");
            //////////////////UglyEncoder.Add('p', "!\".");
            //////////////////UglyEncoder.Add('P', "))%");
            //////////////////UglyEncoder.Add('t', "\"-\"");
            //////////////////UglyEncoder.Add('u', "\".!");
            //////////////////UglyEncoder.Add('U', "%))");
            //////////////////UglyEncoder.Add('V', ")!'");
            //////////////////UglyEncoder.Add('v', "-\"\"");
            //////////////////UglyEncoder.Add('W', ")#%");
            //////////////////UglyEncoder.Add('w', ".!\"");
            //////////////////UglyEncoder.Add('x', ".\"!");
            //////////////////////UglyEncoder.Add('X', "#%)");
            //////////////////UglyEncoder.Add('X', "+!%");
            //////////////////UglyEncoder.Add('y', "%%-");
            //////////////////UglyEncoder.Add('Y', ")%#");
            //////////////////UglyEncoder.Add('z', "%-%");
            //////////////////UglyEncoder.Add('E', "#%#");
            //////////////////UglyEncoder.Add('f', "!(\"");

        }

    }
}

