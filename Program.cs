using System;

namespace sathomework_5_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Input Image data address : ");
            string ImageData = Console.ReadLine();
            double[,] imageDataArray = ReadImageDataFromFile(ImageData);

            Console.Write("Input Convolution Kernel address : ");
            string ConvolutionKer = Console.ReadLine(); ;
            double[,] convolutionArray = ReadImageDataFromFile(ConvolutionKer);

            Console.Write("Output address : ");
            string Output = Console.ReadLine();

            int NewCollumn = imageDataArray.GetLength(0) + convolutionArray.GetLength(0) - 1;
            int NewRow = imageDataArray.GetLength(1) + convolutionArray.GetLength(1) - 1;

            double[,] expandedDataArray = amplifyDataArray(imageDataArray, NewCollumn, NewRow, imageDataArray.GetLength(0), imageDataArray.GetLength(1), convolutionArray.GetLength(0), convolutionArray.GetLength(1));
            double[,] OutputImage = Convolution(expandedDataArray, convolutionArray, imageDataArray.GetLength(0), imageDataArray.GetLength(1));
            WriteImageDataToFile(Output, OutputImage);

        }
        static double[,] amplifyDataArray(double[,] imageDataArray, int expandedCollumn, int expandedRow, int dataArrayCollumn, int dataArrayRow, int convolutionCollumn, int convolutionRow)
        {
            double[,] expandedDataArray = new double[expandedCollumn, expandedRow];
            for (int i = 0; i < expandedCollumn; i++)
            {
                for (int j = 0; j < expandedRow; j++)
                {
                    expandedDataArray[i, j] = imageDataArray[(i + (dataArrayCollumn - 1)) % dataArrayCollumn, (j + (dataArrayRow - 1)) % dataArrayRow];
                }
            }
            return expandedDataArray;
        }
        static double[,] Convolution(double[,] expandedDataArray, double[,] convolutionArray, int dataArrayCollumn, int dataArrayRow)
        {
            double[,] outputImageArray = new double[dataArrayCollumn, dataArrayRow];
            for (int i = 0; i < dataArrayCollumn; i++)
            {
                for (int j = 0; j < dataArrayRow; j++)
                {
                    for (int y = 0; y < convolutionArray.GetLength(0); y++)
                    {
                        for (int t = 0; t < convolutionArray.GetLength(1); t++)
                        {
                            outputImageArray[i, j] += expandedDataArray[i + y, j + t] * convolutionArray[y, t];
                        }
                    }
                }
            }
            return outputImageArray;
        }
        static double[,] ReadImageDataFromFile(string imageDataFilePath)
        {
            string[] lines = System.IO.File.ReadAllLines(imageDataFilePath);
            int imageHeight = lines.Length;
            int imageWidth = lines[0].Split(',').Length;
            double[,] imageDataArray = new double[imageHeight, imageWidth];

            for (int i = 0; i < imageHeight; i++)
            {
                string[] items = lines[i].Split(',');
                for (int j = 0; j < imageWidth; j++)
                {
                    imageDataArray[i, j] = double.Parse(items[j]);
                }
            }
            return imageDataArray;
        }
        static void WriteImageDataToFile(string imageDataFilePath, double[,] imageDataArray)
        {
            string imageDataString = "";
            for (int i = 0; i < imageDataArray.GetLength(0); i++)
            {
                for (int j = 0; j < imageDataArray.GetLength(1) - 1; j++)
                {
                    imageDataString += imageDataArray[i, j] + ", ";
                }
                imageDataString += imageDataArray[i, imageDataArray.GetLength(1) - 1];
                imageDataString += "\n";
            }
            System.IO.File.WriteAllText(imageDataFilePath, imageDataString);

            Console.ReadLine();
        }
    }
}
