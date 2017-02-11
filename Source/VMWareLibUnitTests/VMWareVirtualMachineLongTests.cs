using System;
using System.Collections.Generic;
using NUnit.Framework;
using Vestris.VMWareLib;
using System.Configuration;
using System.IO;
using System.Drawing;
using System.Threading;
using System.Text;
using Interop.VixCOM;

namespace Vestris.VMWareLibUnitTests
{
    [TestFixture]
    public class VMWareVirtualMachineLongTests : VMWareUnitTest
    {
        public override void SetUp()
        {
            if (!_test.Config.RunLongTests)
                Assert.Ignore("Skipping, long tests disabled.");
        }

        [Test]
        public void TestReset()
        {
            foreach (VMWareVirtualMachine virtualMachine in _test.PoweredVirtualMachines)
            {
                // hardware reset
                ConsoleOutput.WriteLine("Reset ...");
                virtualMachine.Reset();
                Assert.AreEqual(true, virtualMachine.IsRunning);
                ConsoleOutput.WriteLine("Wait ...");
                virtualMachine.WaitForToolsInGuest();
            }
        }

        [Test]
        public void TestSuspend()
        {
            foreach (VMWareVirtualMachine virtualMachine in _test.PoweredVirtualMachines)
            {
                ConsoleOutput.WriteLine("Suspend ...");
                virtualMachine.Suspend();
                Assert.AreEqual(false, virtualMachine.IsPaused);
                Assert.AreEqual(true, virtualMachine.IsSuspended);
                ConsoleOutput.WriteLine("Power ...");
                virtualMachine.PowerOn();
                ConsoleOutput.WriteLine("Wait ...");
                virtualMachine.WaitForToolsInGuest();
            }
        }
    }
}
