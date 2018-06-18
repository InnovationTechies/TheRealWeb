<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TheRealWebCam._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>The Web Cam</h1>
        <asp:Image ID="imgWeb" runat="server" />

        <object width="450" height="200"
            param name="movie1" value="WebcamResources/save_picture.swf"
            embed src="WebcamResources/save_picture.swf" width="450"
            height="200">
        </object>


    </div>  

    <div class="row">
        <div class="col-md-4">
            <p>
                <asp:Button ID="btnStart" runat="server" OnClick="btnStart_Click" Text="Start" />
            </p>
             <p>
                 <asp:Button ID="btnStop" runat="server" OnClick="btnStop_Click" Text="Stop" />
            </p>

            
             <p>
                 <asp:Button ID="btnTakePhoto" runat="server" OnClick="btnTakePhoto_Click" Text="Take Photo" />
            </p>
        </div>
        

    </div>

</asp:Content>
