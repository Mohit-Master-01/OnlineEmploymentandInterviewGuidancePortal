<%@ Page Title="" Language="C#" MasterPageFile="~/User/UserMaster.Master" AutoEventWireup="true" CodeBehind="ResumeBuild.aspx.cs" Inherits="OnlineJobPortal.User.ResumeBuild" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section>
        <div class="container pt-50 pb-40">
            <div class="row">
                <div class="col-12 pb-20">
                    <asp:Label ID="lblMsg" runat="server" Visible="false"></asp:Label>
                </div>
                <div class="col-12">
                    <h2 class="contact-title text-center ">Build Resume</h2>
                </div>
                <div class="col-lg-12">
                    <div class="form-contact contact_form">
                        <div class="row">
                            <div class="col-12">
                                <h6>Personal Information</h6>
                            </div>

                            <div class="col-md-6 col-sm-12">
                                <div class="form-group">
                                    <label>Full Name</label>
                                    <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" placeholder="Enter Your FulName" required></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RevFullName" runat="server" ErrorMessage="Name Must Be In Characters" ForeColor="Red" Display="Dynamic" SetFocusError="true"
                                        Font-Size="Small" ValidationExpression="^[a-zA-Z\s]+$" ControlToValidate="txtFullName">
                                    </asp:RegularExpressionValidator>
                                </div>
                            </div>

                            <div class="col-md-6 col-sm-12">
                                <div class="form-group">
                                    <label>Username</label>
                                    <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Enter Unique Username" required></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6 col-sm-12">
                                <div class="form-group">
                                    <label>Address</label>
                                    <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" placeholder="Enter Address" TextMode="MultiLine" required>
                                    </asp:TextBox>
                                </div>
                            </div>


                            <div class="col-md-6 col-sm-12">
                                <div class="form-group">
                                    <label>Mobile number</label>
                                    <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" placeholder="Enter Mobile number" required></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RevMobile" runat="server" ErrorMessage="Mobile no. must have 10 digits" ForeColor="Red" Display="Dynamic" SetFocusError="true"
                                        Font-Size="Small" ValidationExpression="^[0-9]{10}$" ControlToValidate="txtMobile">
                                    </asp:RegularExpressionValidator>
                                </div>
                            </div>

                            <div class="col-md-6 col-sm-12">
                                <label>Select Country</label>
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="true" CssClass="form-control w-100"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvCountry" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="ddlCountry"></asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="col-md-6 col-sm-12">
                                <div class="form-group">
                                    <label>Email</label>
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Enter Your Email" required TextMode="Email"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-12 pt-4">
                                <h6>Education/Resume Information</h6>
                            </div>

                            <div class="col-md-6 col-sm-12" style="top: 30px">
                                <div class="form-group">
                                    <label>10th Percentage/Grade</label>
                                    <asp:TextBox ID="txtTenth" runat="server" CssClass="form-control" placeholder="Ex: 90%"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6 col-sm-12" style="top: 30px">
                                <div class="form-group">
                                    <label>12th Percentage/Grade</label>
                                    <asp:TextBox ID="txtTwelfth" runat="server" CssClass="form-control" placeholder="Ex: 90%"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6 col-sm-12" style="top: 30px">
                                <div class="form-group">
                                    <label>Graduation with Pointer/Grade</label>
                                    <asp:TextBox ID="txtGraduation" runat="server" CssClass="form-control" placeholder="Ex: BTech with 9.2 pointer"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6 col-sm-12" style="top: 30px">
                                <div class="form-group">
                                    <label>Post Graduation with Pointer/Grade</label>
                                    <asp:TextBox ID="txtPostGraduation" runat="server" CssClass="form-control" placeholder="Ex: MTech with 9.5 pointer"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6 col-sm-12" style="top: 30px">
                                <div class="form-group">
                                    <label>PHD with Percentage/Grade</label>
                                    <asp:TextBox ID="txtPhd" runat="server" CssClass="form-control" placeholder="Ex: PHD with Grade"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6 col-sm-12" style="top: 30px">
                                <div class="form-group">
                                    <label>Job Profile/Works On</label>
                                    <asp:TextBox ID="txtWork" runat="server" CssClass="form-control" placeholder="Enter Job Profile"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6 col-sm-12" style="top: 30px">
                                <div class="form-group">
                                    <label>Work Experience</label>
                                    <asp:TextBox ID="txtExperience" runat="server" CssClass="form-control" placeholder="Enter Your Work Experience"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6 col-sm-12" style="top: 30px">
                                <div class="form-group">
                                    <label>Resume</label>
                                    <asp:FileUpload ID="fuResume" runat="server" CssClass="form-control pt-2" ToolTip=".doc, .docx, .pdf extension only" />
                                </div>
                            </div>

                            <div class="col-md-6 col-sm-12" style="top: 30px">
                                <div class="form-group">
                                    <label>Profile Picture</label>
                                    <asp:FileUpload ID="fuProfile" runat="server" CssClass="form-control pt-2" ToolTip=".jpg, .jpeg, .png extension only"/>
                                </div>
                            </div>

                        </div>
                        <div class="form-group mt-3">
                            <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="button button-contactForm boxed-btn" OnClick="btnUpdate_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </section>

</asp:Content>
