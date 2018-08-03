using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Turners.UserPortal.Domain;
using System.Configuration;
using System.Text.RegularExpressions;
using Turners.UserPortal.Helpers;

namespace Turners.UserPortal.Repository
{
    public class BranchesCsvRepository : IBranchesRepository
    {

        private const int BranchNameColumnIndex = 0;
        private const int BranchAliasesColumnIndex = 1;
        private const int BranchAddressColumnIndex = 2;
        private const int BranchPhoneNumberColumnIndex = 3;
        private const int BranchPhoneExtensionColumnIndex = 4;
        private const int BranchFaxNumberColumnIndex = 5;
        private readonly string _filePath;

        public BranchesCsvRepository()
        {
            _filePath = ConfigurationManager.AppSettings.Get("BranchesCsvFilePath");

            if (string.IsNullOrEmpty(_filePath) || Path.GetFileName(_filePath) == null)
            {
                throw new Exception($"Invalid Branches Csv File Path : {_filePath}");
            }
        }

        public async Task<List<Branch>> GetAllBranches()
        {
            return await ParseCsv(_filePath, headerRowCount: 1, pageSize: int.MaxValue);
        }

        private async Task<List<Branch>> ParseCsv(string filePath, int headerRowCount = 0, int page = 1, int pageSize = 100)
        {
            try
            {
                var csvRecords = await GetLinesFromCSV(filePath, headerRowCount, page, pageSize);

                var branches = ParseCSVRecords(csvRecords);

                return branches;
            }
            catch (Exception e)
            {
                throw new Exception($"Error in parsing Branches CSV file {filePath}", e);
            }
        }

        private List<Branch> ParseCSVRecords(IList<string> csvRecords)
        {
            if (csvRecords == null) return new List<Branch>();

            return csvRecords.Select(x =>
            {
                var fields = x.SplitCSV().Select(s => s.Trim()).ToArray();
                var carReg = new Branch
                {
                    Name = fields[BranchNameColumnIndex],
                    Aliases = fields[BranchAliasesColumnIndex],
                    Address = fields[BranchAddressColumnIndex],
                    PhoneNumber = fields[BranchPhoneNumberColumnIndex],
                    Extension = fields[BranchPhoneExtensionColumnIndex],
                    Fax = fields[BranchFaxNumberColumnIndex]
                };

                return carReg;
            }).ToList();
        }


        public static string[] SplitCSV(string input)
        {
            Regex csvSplit = new Regex("(?:^|,)(\"(?:[^\"])*\"|[^,]*)", RegexOptions.Compiled);
            List<string> list = new List<string>();
            string curr = null;
            foreach (Match match in csvSplit.Matches(input))
            {
                curr = match.Value;
                if (0 == curr.Length)
                {
                    list.Add("");
                }

                list.Add(curr.TrimStart(','));
            }

            return list.ToArray();
        }

        private async Task<List<string>> GetLinesFromCSV(string _filePath, int headerRowCount = 0, int page = 1, int pageSize = 100)
        {
            using (StreamReader reader = new StreamReader(File.OpenRead(_filePath)))
            {
                try
                {
                    var lines = new List<string>();

                    page = page > 0 ? page : 1;

                    var numLinesToSkip = (page - 1) * pageSize + headerRowCount;
                    var lineIndex = 0;

                    while (lines.Count < pageSize && !reader.EndOfStream)
                    {
                        var line = await reader.ReadLineAsync();

                        if (lineIndex >= numLinesToSkip)
                        {
                            lines.Add(line);
                        }

                        lineIndex++;
                    }

                    return lines;
                }
                catch (Exception e)
                {
                    throw new Exception($"Error in reading lines from CSV file {_filePath}", e);
                }
                finally
                {
                    try
                    {
                        if (reader != null)
                        {
                            reader.Close();
                        }
                    }
                    catch (Exception e) { }
                }
            }
        }

    }


}
