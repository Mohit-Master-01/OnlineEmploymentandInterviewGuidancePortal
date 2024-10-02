<%@ Page Title="" Language="C#" MasterPageFile="~/User/UserMaster.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="OnlineJobPortal.User.Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
        .clickLink a {
            color: Highlight;
            font-family: "Barlow", sans-serif;
            font-weight: 500;
            font-size: 15px;
        }

            .clickLink a:hover {
                color: #fb246a
            }
    </style>
    <section>
        <div class="container pt-50 pb-40">
            <div class="row">
                <div class="col-12 pb-20">
                    <asp:Label ID="lblMsg" runat="server" Visible="false"></asp:Label>
                </div>
                <div class="col-12">
                    <h2 class="contact-title text-center ">Sign Up</h2>
                </div>
                <div class="col-lg-6 mx-auto">
                    <div class="form-contact contact_form">
                        <div class="row">
                            <div class="col-12">
                                <h6>Login Information</h6>
                            </div>
                            <div class="col-12">
                                <div class="form-group">
                                    <label>Username</label>
                                    <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Enter Unique Username" required></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <label>Password</label>
                                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" placeholder="Enter Password" TextMode="Password" required></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <label>Confirm Password</label>
                                    <asp:TextBox ID="txtConfirmPass" runat="server" CssClass="form-control" placeholder="Enter  Confirm Password" TextMode="Password" required></asp:TextBox>
                                    <asp:CompareValidator ID="CvPassword" runat="server" ErrorMessage="Password & confirm password should be same"
                                        ControlToCompare="txtPassword" ControlToValidate="txtConfirmPass" ForeColor="Red" Display="Dynamic" SetFocusError="true"
                                        Font-Size="Small">
                                    </asp:CompareValidator>
                                </div>
                            </div>
                            <div class="col-12">
                                <h6>Personal Information</h6>
                            </div>
                            <div class="col-12">
                                <div class="form-group">
                                    <label>Full Name</label>
                                    <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" placeholder="Enter Your FulName" required></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RevFullName" runat="server" ErrorMessage="Name Must Be In Characters" ForeColor="Red" Display="Dynamic" SetFocusError="true"
                                        Font-Size="Small" ValidationExpression="^[a-zA-Z\s]+$" ControlToValidate="txtFullName">
                                    </asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="col-12">
                                <div class="form-group">
                                    <label>Address</label>
                                    <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" placeholder="Enter Address" TextMode="MultiLine" required>
                                    </asp:TextBox>

                                </div>
                            </div>
                            <div class="col-12">
                                <div class="form-group">
                                    <label>Mobile number</label>
                                    <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" placeholder="Enter Mobile number" required></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RevMobile" runat="server" ErrorMessage="Mobile no. must have 10 digits" ForeColor="Red" Display="Dynamic" SetFocusError="true"
                                        Font-Size="Small" ValidationExpression="^[0-9]{10}$" ControlToValidate="txtMobile">
                                    </asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <label>Select Country</label>
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" CssClass="form-control w-100"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvCountry" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="ddlCountry"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <label>Select State</label>
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control w-100">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvState" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="ddlState"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-sm-6" style="top: 30px">
                                <div class="form-group">
                                    <label>Email</label>
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Enter Your Email" required TextMode="Email"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-6" style="top: 30px">
                                <div class="form-group">
                                    <label>Profile Picture</label>
                                    <asp:FileUpload ID="fuProfile" runat="server" CssClass="form-control" ToolTip=".jpg, .jpeg, .png extension only"/>
                                </div>
                            </div>
                        </div>
                        <div class="form-group mt-3">
                            <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="button button-contactForm boxed-btn mr-4" OnClick="btnRegister_Click" />
                            <span class="clickLink"><a href="../User/Login.aspx">Already User?  Click Here..</a></span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>

</asp:Content>
