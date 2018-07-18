using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;
using BlueTimeMeter;
using Xunit.Sdk;

namespace BlueTimeMeter.Tests
{
    public class BasicTest
    {

        [Fact]
        public void If_we_make_start_and_stop_with_sleep_of_5sec_stop_result_should_be_more_or_equal_to_5000_milisec()
        {
            Measure.Start();
            System.Threading.Thread.Sleep(5000);

            Assert.True(Measure.Stop().Result >= 5000);
        }

        [Fact]
        public void If_we_dont_specify_a_filepath_defaul_we_are_writing_measures_result_on_default_filePath()
        {
            var preDate = DateTime.Now;
            Measure.Start();
            var defaultFilePath = "../../../Tiempos.log";
            Measure.Stop();
            Task.WaitAll();
            var postDate = System.IO.File.GetLastWriteTime(defaultFilePath);
            
            Assert.True(File.Exists(defaultFilePath));
            Assert.True(postDate > preDate);
        }

        [Fact]
        public void We_can_change_only_one_time_filepath()
        {
            var newPath = "../../NewLog.log";
            Measure.SetPath(newPath);
            var anothernewPath = "../../AlteredNewLog.log";
            Measure.SetPath(anothernewPath);
            Measure.Start();
            Measure.Stop();
            Task.WaitAll();

            Assert.True(File.Exists(newPath));
            Assert.False(File.Exists(anothernewPath));

        }
    }
}
