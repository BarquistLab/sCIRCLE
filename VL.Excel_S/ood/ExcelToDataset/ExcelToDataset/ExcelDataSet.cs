using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using ExcelDataReader;
using System.IO;

namespace Excel
{
    public class ExcelDataSet
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename">Gets or sets a value indicating whether to set the DataColumn.DataType property in a second pass.</param>
        /// <param name="_UseColumnDataType"></param>
        /// <param name="_EmptyColumnNamePrefix">Gets or sets a value indicating the prefix of generated column names.</param>
        /// <param name="_UseHeaderRow">Gets or sets a value indicating whether to use a row from the data as column names.</param>
        /// <returns></returns>
        public DataSet ExcelToDataset(string filename, bool _UseColumnDataType = true, string _EmptyColumnNamePrefix = "Col", bool _UseHeaderRow = false)
        {
            using (var stream = File.Open(filename, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    return reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        // Gets or sets a value indicating whether to set the DataColumn.DataType property in a second pass.
                        UseColumnDataType = _UseColumnDataType,

                        // Gets or sets a callback to obtain configuration options for a DataTable. 
                        ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                        {

                            // Gets or sets a value indicating the prefix of generated column names.
                            EmptyColumnNamePrefix = _EmptyColumnNamePrefix,

                            // Gets or sets a value indicating whether to use a row from the 
                            // data as column names.
                            UseHeaderRow = _UseHeaderRow,

                            // Gets or sets a callback to determine which row is the header row. 
                            // Only called when UseHeaderRow = true.
                            ReadHeaderRow = (rowReader) => {
                                // F.ex skip the first row and use the 2nd row as column headers:
                                rowReader.Read();
                            },

                            // Gets or sets a callback to determine whether to include the 
                            // current row in the DataTable.
                            FilterRow = (rowReader) => {
                                return true;
                            },

                            // Gets or sets a callback to determine whether to include the specific
                            // column in the DataTable. Called once per column after reading the 
                            // headers.
                            FilterColumn = (rowReader, columnIndex) => {
                                return true;
                            }
                        }
                    });
                }
            }
        }

        public DataSet ExcelToDataset(string filename, ExcelReaderConfiguration config)
        {
            using (var stream = File.Open(filename, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream, config))
                {
                    return reader.AsDataSet();
                }
            }
        }

        public DataSet ExcelToDataset(string filename, ExcelDataSetConfiguration datasetConfig)
        {
            //var e = datasetConfig.ConfigureDataTable;
            using (var stream = File.Open(filename, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    return reader.AsDataSet(datasetConfig);
                }
            }
        }

        public DataSet ExcelToDataset(string filename, ExcelReaderConfiguration readerConfig, ExcelDataSetConfiguration datasetConfig)
        {
            using (var stream = File.Open(filename, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream, readerConfig))
                {
                    return reader.AsDataSet(datasetConfig);
                }
            }
        }

        //public DataSet ExcelToDataTable(string filename, ExcelDataTableConfiguration datatableConfig)
        //{
        //    using (var stream = File.Open(filename, FileMode.Open, FileAccess.Read))
        //    {
        //        using (var reader = ExcelReaderFactory.CreateReader(stream))
        //        {
        //            return reader.AsDataTable(datatableConfig);
        //        }
        //    }
        //}

        public ExcelReaderConfiguration CreateExcelReaderConfiguration(Encoding encoding)
        {
            ExcelReaderConfiguration c = new ExcelReaderConfiguration();
            c.FallbackEncoding = encoding;
            return c;
        }

        public ExcelDataSetConfiguration CreateExcelDataSetConfiguration(bool ColumnDatatype, Func<IExcelDataReader, ExcelDataTableConfiguration> edtc)
        {
            ExcelDataSetConfiguration c = new ExcelDataSetConfiguration();
            c.UseColumnDataType = ColumnDatatype;
            //c.ConfigureDataTable = edtc;
            //c.ConfigureDataTable = new Func<IExcelDataReader, ExcelDataTableConfiguration>();
            return c;
        }

        public ExcelDataTableConfiguration CreateExcelDataTableConfiguration(string ColumnPrefix, bool UseHeaderRow)
        {
            ExcelDataTableConfiguration e = new ExcelDataTableConfiguration();
            e.EmptyColumnNamePrefix = ColumnPrefix;
            e.UseHeaderRow = UseHeaderRow;

            //Func<IExcelDataReader, ExcelDataTableConfiguration> ff = new Func<IExcelDataReader, ExcelDataTableConfiguration>(true);
            return e;
        }



    }


    //public DataSet ExcelToDataset(string filename)
    //{
    //    using (var stream = File.Open(filename, FileMode.Open, FileAccess.Read))
    //    {

    //        // Auto-detect format, supports:
    //        //  - Binary Excel files (2.0-2003 format; *.xls)
    //        //  - OpenXml Excel files (2007 format; *.xlsx)
    //        using (var reader = ExcelReaderFactory.CreateReader(stream))
    //        {
    //            /*
    //            // Choose one of either 1 or 2:

    //            // 1. Use the reader methods
    //            do
    //            {
    //                while (reader.Read())
    //                {
    //                    // reader.GetDouble(0);
    //                }
    //            } while (reader.NextResult());
    //            */


    //            // 2. Use the AsDataSet extension method
    //            return reader.AsDataSet();

    //            // The result of each spreadsheet is in result.Tables
    //        }

    //    }
    //}


    //public static class ExcelDataReaderExtensions
    //{
    //    public static DataSet AsDataSet(this IExcelDataReader self, ExcelDataSetConfiguration configuration = null)
    //    {
    //        if (configuration == null)
    //        {
    //            configuration = new ExcelDataSetConfiguration();
    //        }

    //        self.Reset();

    //        var result = new DataSet();
    //        do
    //        {
    //            var tableConfiguration = configuration.ConfigureDataTable != null
    //                ? configuration.ConfigureDataTable(self)
    //                : null;

    //            if (tableConfiguration == null)
    //            {
    //                tableConfiguration = new ExcelDataTableConfiguration();
    //            }

    //            var table = AsDataTable(self, tableConfiguration);
    //            result.Tables.Add(table);
    //        }
    //        while (self.NextResult());

    //        result.AcceptChanges();

    //        if (configuration.UseColumnDataType)
    //        {
    //            FixDataTypes(result);
    //        }

    //        self.Reset();

    //        return result;
    //    }
    //}
    //public class ExcelDataSetConfiguration
    //{
    //    /// <summary>
    //    /// Gets or sets a value indicating whether to set the DataColumn.DataType property in a second pass.
    //    /// </summary>
    //    public bool UseColumnDataType { get; set; } = true;

    //    /// <summary>
    //    /// Gets or sets a callback to obtain configuration options for a DataTable. 
    //    /// </summary>
    //    public Func<IExcelDataReader, ExcelDataTableConfiguration> ConfigureDataTable { get; set; }
    //}

    //public class ExcelDataTableConfiguration
    //{
    //    /// <summary>
    //    /// Gets or sets a value indicating the prefix of generated column names.
    //    /// </summary>
    //    public string EmptyColumnNamePrefix { get; set; } = "Column";

    //    /// <summary>
    //    /// Gets or sets a value indicating whether to use a row from the data as column names.
    //    /// </summary>
    //    public bool UseHeaderRow { get; set; } = false;

    //    /// <summary>
    //    /// Gets or sets a callback to determine which row is the header row. Only called when UseHeaderRow = true.
    //    /// </summary>
    //    public Action<IExcelDataReader> ReadHeaderRow { get; set; }

    //    /// <summary>
    //    /// Gets or sets a callback to determine whether to include the current row in the DataTable.
    //    /// </summary>
    //    public Func<IExcelDataReader, bool> FilterRow { get; set; }

    //    /// <summary>
    //    /// Gets or sets a callback to determine whether to include the specific column in the DataTable. Called once per column after reading the headers.
    //    /// </summary>
    //    public Func<IExcelDataReader, int, bool> FilterColumn { get; set; }
    //}
}
