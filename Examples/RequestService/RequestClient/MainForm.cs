using System;
using System.ServiceModel;
using System.Windows.Forms;
using RequestClient.RequestServiceReference;



namespace RequestClient
{
    public partial class MainForm : Form
    {
        private RequestServiceClient _requestService;



        public MainForm()
        {
            InitializeComponent();
            _requestService = new RequestServiceClient("NetTcpBinding_IRequestService");

            requestTypeCb.Items.Add(RequestType.JuridicalTracking);
            requestTypeCb.Items.Add(RequestType.Meeting);
            requestTypeCb.Items.Add(RequestType.Seminar);

            requestTypeCb.SelectedIndex = 2;
        }



        // USER ACTIONS ///////////////////////////////////////////////////////////////////////////
        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (_requestService.ConnectionRequest())
                    toolStripStatusLbl.Text = "Connected";
            }
            catch (Exception)
            {
                toolStripStatusLbl.Text = "Disconnected";
            }
        }
        private void addUserBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (_requestService.State == CommunicationState.Faulted)
                {
                    _requestService.Abort();
                    _requestService = new RequestServiceClient("NetTcpBinding_IRequestService");
                    toolStripStatusLbl.Text = "Connected";
                }

                var userId = _requestService.AddUser(firstNameTb.Text.Trim(), lastNameTb.Text.Trim());
                if (userId != Guid.Empty)
                {
                    toolStripLastOperationStatusLbl.Text = "User adding, success";
                    userIdTb.Text = userId.ToString();
                }
                else
                {
                    toolStripLastOperationStatusLbl.Text = "User adding, fail";
                }
            }
            catch (Exception)
            {
                toolStripLastOperationStatusLbl.Text = "User adding, fail";
                toolStripStatusLbl.Text = "Disconnected";
            }
        }
        private void addRequestBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (_requestService.State == CommunicationState.Faulted)
                {
                    _requestService.Abort();
                    _requestService = new RequestServiceClient("NetTcpBinding_IRequestService");
                    toolStripStatusLbl.Text = "Connected";
                }

                var userId = Guid.Parse(userIdTb.Text.Trim());
                var operationResult = false;

                switch (requestTypeCb.SelectedIndex)
                {
                    case 0:
                        operationResult = SendJuridicalTrackingRequest(userId);
                        break;
                    case 1:
                        operationResult = SendMeetingRequest(userId);
                        break;
                    case 2:
                        operationResult = SendSeminarRequest(userId);
                        break;
                }

                if (operationResult)
                {
                    toolStripLastOperationStatusLbl.Text = "Request adding, success";
                }
                else
                {
                    toolStripLastOperationStatusLbl.Text = "Request adding, fail";
                }
            }
            catch (FormatException)
            {
                toolStripLastOperationStatusLbl.Text = "Incorrect user ID";
            }
            catch (ArgumentNullException)
            {
                toolStripLastOperationStatusLbl.Text = "Field user ID is empty";
            }
            catch (Exception)
            {
                toolStripLastOperationStatusLbl.Text = "Request adding, fail";
                toolStripStatusLbl.Text = "Disconnected";
            }
        }



        // FUNCTIONS //////////////////////////////////////////////////////////////////////////////
        private Boolean SendMeetingRequest(Guid userId)
        {
            var request = new MeetingRequest
            {
                Message = messageTb.Text.Trim()
            };
            var requestParameters = new RequestParameters
            {
                Request = request,
                UserId = userId
            };
            return _requestService.AddRequest(RequestType.Meeting, requestParameters);
        }
        private Boolean SendJuridicalTrackingRequest(Guid userId)
        {
            var request = new JuridicalTrackingRequest
            {
                Message = messageTb.Text.Trim()
            };
            var requestParameters = new RequestParameters
            {
                Request = request,
                UserId = userId
            };
            return _requestService.AddRequest(RequestType.JuridicalTracking, requestParameters);
        }
        private Boolean SendSeminarRequest(Guid userId)
        {
            var request = new SeminarRequest
            {
                Message = messageTb.Text.Trim()
            };
            var requestParameters = new RequestParameters
            {
                Request = request,
                UserId = userId
            };
            return _requestService.AddRequest(RequestType.Seminar, requestParameters);
        }
    }
}
