using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Mathematics
{
    public class Math
    {
        private static bool Calculation(double dCopyExpresion, double Epsilon)
        {
            return System.Math.Abs(dCopyExpresion) <= Epsilon;
        }

        public static bool Compare(double first, double second, double epsilon)
        {
            return Calculation(first - second, epsilon);
        }
    }
    public class ReportAction
    {
        public static void PositiveResults()
        {
            Ranorex.Report.Success("Sucessfully Performed");
        }

        public static void NegativeReport(double dActual, double dHardcoded, int iErrorLine)
        {
            string errorLine = "Error on Line: " + (iErrorLine + 1);
            Ranorex.Report.Error(errorLine);

            string actualValue = "Actual Value is: " + dActual;
            Ranorex.Report.Error(actualValue);

            string hardcodedValue = "Hardcoded Value is: " + dHardcoded;
            Ranorex.Report.Error(hardcodedValue);
        }
    }

    class UtilityRun
    {
        public static void ConstructCalculations(string Actual, string Hardcoded)
        {
            string[] strReadActualValues = File.ReadAllLines(Actual);
            string[] strReadHardcodedVAlues = File.ReadAllLines(Hardcoded);

            int iActualRows = strReadActualValues.Length;
            int iHardcodedRows = strReadHardcodedVAlues.Length;

            bool bAllResultsOK = true;

            for (int i = 0; i < iActualRows; i++)
            {
                double dActualResults;
                double dHardcodedResults;
                Double.TryParse(strReadActualValues[i], out dActualResults);
                Double.TryParse(strReadHardcodedVAlues[i], out dHardcodedResults);

                try
                {

                    bool bSimilar = Math.Compare(dActualResults, dHardcodedResults, 0.000);

                    if (!bSimilar)
                    {
                        bAllResultsOK = false;
                        ReportAction.NegativeReport(dActualResults, dHardcodedResults, i);
                    }
                }

                catch (Exception)
                {

                }
            }

            if (bAllResultsOK)
            {
                ReportAction.PositiveResults();
            }

        }
    }
}
