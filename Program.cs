using MessageEncrypter.Crypto;
using MessageEncrypter.Crypto.RSA;
using MessageEncrypter.UserInterface;
using System;
using System.Threading;
using System.Windows.Forms;

namespace MessageEncrypter
{
    class Program
    {
        private const string UnknownExceptionErrorMessage = "An unknown exception occured.";

        private readonly ICryptoFacade cryptoFacade;

        public bool IsApplicationInDebugMode
        {
            get
            {
#if DEBUG
                return true;
#else
                return false;
#endif
            }
        }

        private Program(ICryptoFacade cryptoFacade)
        {
            this.cryptoFacade = cryptoFacade;
        }

        private void Run()
        {
            if (!IsApplicationInDebugMode)
            {
                AddErrorHandlersForUnhandledExceptions();
            }
            ConfigureApplicationStyles();
            CreateAndRunMainForm();
        }

        private void ConfigureApplicationStyles()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
        }

        private void AddErrorHandlersForUnhandledExceptions()
        {
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += AppDomain_UnhandledException;
        }

        private void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            HandleUnhandledException(e.Exception);
        }

        private void AppDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            HandleUnhandledException((Exception)e.ExceptionObject);
        }

        private void HandleUnhandledException(Exception exception)
        {
            var applicationException = exception as ApplicationException;
            if (applicationException != null)
            {
                HandleApplicationException(applicationException);
            }
            else
            {
                HandleUnknownException(exception);
            }
        }

        private void HandleApplicationException(ApplicationException applicationException)
        {
            MessageBox.Show(applicationException.Message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void HandleUnknownException(Exception exception)
        {
            MessageBox.Show(UnknownExceptionErrorMessage, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void CreateAndRunMainForm()
        {
            var mainForm = new MessageEncrypterForm(cryptoFacade);
            Application.Run(mainForm);
        }

        [STAThread]
        static void Main()
        {
            var cryptoImplementation = new RsaCryptoImplementation();
            var cryptoFacade = CryptoFacade.Create(cryptoImplementation);
            var program = new Program(cryptoFacade);
            program.Run();
        }
    }
}
