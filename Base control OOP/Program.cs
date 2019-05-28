using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base_control_OOP
{
    abstract class File
    {
        public string Extension { get; set; }
        public string Size { get; set; }
        public string Name { get; set; }
        abstract public void ConsoleLog();
        public static File Parse(string secondPart, string firstPart)
        {
            if (firstPart == "Text" || firstPart == "Image")
            {
                string[] secondPartArr = secondPart.Split(';');

                int bracketOpenIndex = secondPartArr[0].IndexOf('(');
                int braketCloseIndex = secondPartArr[0].IndexOf(')');
                string extension = secondPartArr[0].Substring(bracketOpenIndex - 3, 3);

                string size = secondPartArr[0].Substring(bracketOpenIndex + 1, braketCloseIndex - bracketOpenIndex - 1);

                if (firstPart == "Text")
                {
                    return new TextFile(secondPartArr[0].Split('.')[0], extension, size, secondPartArr[1]);
                    //return new TextFile("", "", "", "");
                }
                else
                {
                    return new ImageFile(secondPartArr[0].Split('.')[0], secondPartArr[1], size, extension);
                    //return new ImageFile("", "", "", "");
                }


            }
            else
            {

                string[] secondPartArr = secondPart.Split(';');

                int bracketOpenIndex = secondPartArr[0].IndexOf('(');
                int braketCloseIndex = secondPartArr[0].IndexOf(')');
                string extension = secondPartArr[0].Substring(bracketOpenIndex - 3, 3);

                string size = secondPartArr[0].Substring(bracketOpenIndex + 1, braketCloseIndex - bracketOpenIndex - 1);


                return new MovieFile(secondPartArr[0].Split('.')[0] + secondPartArr[0].Split('.')[1], secondPartArr[2], secondPartArr[1], size, extension);
            }



        }
    }
    class TextFile : File
    {
        public string Content;
        public TextFile (string name, string extension, string size, string content)
        {
            Name = name;
            Extension = extension;
            Size = size;
            Content = content;
        }

        public override void ConsoleLog()
        {
            Console.WriteLine($"{Name + Extension}");
            Console.WriteLine($"Size:{Size}");
            Console.WriteLine($"Content:{Content}");
        }
    }
    class MovieFile : File
    {
        public string Lenght { get; set; }
        public string Resolution { get; set; }
        public MovieFile(string name ,string lenght, string resolution , string size , string extension)
        {
            Name = name;
            Lenght = lenght;
            Resolution = resolution;
            Size = size;
            Extension = extension;
        }

        public override void ConsoleLog()
        {
            Console.WriteLine($"{Name + Extension}");
            Console.WriteLine($"Extension:{Extension}");
            Console.WriteLine($"Size:{Size}");
            Console.WriteLine($"Resolution:{Resolution}");
            Console.WriteLine($"Length:{Lenght}");
        }   
    }
    class ImageFile : File
    {
        public string Resolution { get; set; }

        public ImageFile(string name, string resolution, string size, string extension)
        {
            Name = name;
            Resolution = resolution;
            Size = size;
            Extension = extension;
        }

        public override void ConsoleLog()
        {
            Console.WriteLine($"{Name + Extension}");
            Console.WriteLine($"Extension:{Extension}");
            Console.WriteLine($"Size:{Size}");
            Console.WriteLine($"Resolution:{Resolution}");         
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string inputString = @"Text: file.txt(6B); Some string content
Image: img.bmp(19MB); 1920х1080
Text:data.txt(12B); Another string
Text:data1.txt(7B); Yet another string
Movie:logan.2017.mkv(19GB); 1920х1080; 2h12m";

            string[] StringArr = inputString.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            int amountTxt = 0;
            int amountMovies = 0;
            int amountImages = 0;

            File[] files = new File[StringArr.Length];

            for (int i = 0; i < StringArr.Length; i++)
            {
                string strToParse = StringArr[i];
                int semIndex = strToParse.IndexOf(':');
                string firstPart = strToParse.Substring(0, semIndex);
                string secondPart = strToParse.Substring(semIndex, strToParse.Length - semIndex).Remove(0,1);                               
                files[i] = File.Parse(secondPart, firstPart);        
            }

            for (int i = 0; i < files.Length; i++)
            {

                if (files[i].Extension == "txt")
                {
                    amountTxt++;
                }
                else if (files[i].Extension == "bmp")
                {
                    amountImages++;
                }
                else
                {
                    amountMovies++;
                }

            }

            TextFile[] textFilesArr = new TextFile[amountTxt];
            MovieFile[] movieFilesArr = new MovieFile[amountMovies];
            ImageFile[] imageFilesArr = new ImageFile[amountImages];

            int txtFileI = 0;
            int movieFileI = 0;
            int imagesFileI = 0;

            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Extension == "txt")
                {
                    textFilesArr[txtFileI] = (TextFile)files[i];
                    txtFileI++;
                }
                else if (files[i].Extension == "bmp")
                {
                    imageFilesArr[imagesFileI] = (ImageFile)files[i];
                    imagesFileI++;
                }
                else
                {
                    movieFilesArr[movieFileI] = (MovieFile)files[i];
                    movieFileI++;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                if(i == 0)
                {
                    Console.WriteLine("**********TXT FILES**********");
                    foreach (TextFile item in textFilesArr)
                        item.ConsoleLog();
                }else if(i == 1)
                {
                    Console.WriteLine("**********IMAGES FILES**********");
                    foreach (ImageFile item in imageFilesArr)
                        item.ConsoleLog();
                }
                else
                {
                    Console.WriteLine("**********Movie FILES**********");
                    foreach (MovieFile item in movieFilesArr)
                        item.ConsoleLog();
                }
            }
            Console.ReadLine();          
        }
    }
}