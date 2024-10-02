<%@ Page Title="" Language="C#" MasterPageFile="~/User/UserMaster.Master" AutoEventWireup="true" CodeBehind="InterviewTactics.aspx.cs" Inherits="OnlineJobPortal.User.InterviewTactics" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.0.3/css/font-awesome.css"
        rel="stylesheet" type="text/css" />

    <style type="text/css">
        .checkbox {
            padding-left: 20px;
        }


            .checkbox label {
                display: inline-block;
                vertical-align: middle;
                position: relative;
                padding-left: 5px;
            }


                .checkbox label::before {
                    content: "";
                    display: inline-block;
                    position: absolute;
                    width: 17px;
                    height: 17px;
                    left: 0;
                    margin-left: -20px;
                    border: 1px solid #cccccc;
                    border-radius: 3px;
                    background-color: #fff;
                    -webkit-transition: border 0.15s ease-in-out, color 0.15s ease-in-out;
                    -o-transition: border 0.15s ease-in-out, color 0.15s ease-in-out;
                    transition: border 0.15s ease-in-out, color 0.15s ease-in-out;
                }


                .checkbox label::after {
                    display: inline-block;
                    position: absolute;
                    width: 16px;
                    height: 16px;
                    left: 0;
                    top: 0;
                    margin-left: -20px;
                    padding-left: 3px;
                    padding-top: 1px;
                    font-size: 11px;
                    color: #FF4357;
                }


            .checkbox input[type="checkbox"] {
                opacity: 0;
                z-index: 1;
            }


                .checkbox input[type="checkbox"]:checked + label::after {
                    font-family: "FontAwesome";
                    content: "\f00c";
                }


        .checkbox-primary input[type="checkbox"]:checked + label::before {
            background-color: #FF4357;
            border-color: #FF4357;
        }


        .checkbox-primary input[type="checkbox"]:checked + label::after {
            color: #fff;
        }
    </style>

    <style>
        .radiobuttonlist {
            font: 12px Verdana, sans-serif;
            color: #000; /* non selected color */
        }


            .radiobuttonlist input {
                width: 0px;
                height: 20px;
            }


            .radiobuttonlist label {
                color: #FF4357;
                background-color: #FFF;
                padding-left: 6px;
                padding-right: 6px;
                padding-top: 2px;
                padding-bottom: 2px;
                border: 1px solid #FF4357;
                border-radius: 10%;
                margin: 0px 0px 0px 0px;
                white-space: nowrap;
                clear: left;
                margin-right: 5px;
            }


            .radiobuttonlist span.selectedradio label {
                background-color: #FF4357;
                color: #FFF;
                font-weight: bold;
                border-bottom-color: #F3F2E7;
                padding-top: 4px;
            }


            .radiobuttonlist label:hover {
                color: #CC3300;
                background: #D1CFC2;
            }


        .radiobuttoncontainer {
            position: relative;
            z-index: 1;
        }


        .radiobuttonbackground {
            position: relative;
            z-index: 0;
            border: solid 1px #AcA899;
            padding: 10px;
            background-color: #F3F2E7;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="job-listing-area pt-50 pb-120">

        <div class="container">

            <div class="row">

                <!-- Left content -->

                <div class="col-xl-2 col-lg-3 col-md-4">

                    <div class="row">

                        <div class="col-12">

                            <div class="small-section-tittle2 mb-45">

                                <div class="ion">

                                    <svg
                                        xmlns="http://www.w3.org/2000/svg"
                                        xmlns:xlink="http://www.w3.org/1999/xlink"
                                        width="20px" height="12px">

                                        <path fill-rule="evenodd" fill="rgb(27, 207, 107)"
                                            d="M7.778,12.000 L12.222,12.000 L12.222,10.000 L7.778,10.000 L7.778,12.000 ZM-0.000,-0.000 L-0.000,2.000 L20.000,2.000 L20.000,-0.000 L-0.000,-0.000 ZM3.333,7.000 L16.667,7.000 L16.667,5.000 L3.333,5.000 L3.333,7.000 Z" />

                                    </svg>

                                </div>

                                <h4>Filter Fields</h4>

                            </div>

                        </div>

                    </div>

                    <!-- Job Category Listing start -->

                    <div class="job-category-listing mb-50 pb-0">

                        <!-- single one -->

                        <div class="single-listing">

                            <div class="small-section-tittle2">

                                <h4>IT Field</h4>

                            </div>

                            <!-- Select job items start -->

                            <div class="select-job-items2">

                                <asp:DropDownList ID="ddlField" runat="server" AutoPostBack="true" CssClass="form-control w-100" OnSelectedIndexChanged="ddlField_SelectedIndexChanged" AppendDataBoundItems="true" DataTextField="Title" DataValueField="g_id">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                </asp:DropDownList>

                            </div>

                            <!--  Select job items End-->

                            <!-- select-Categories start -->

                            <div class="select-Categories pt-80 pb-50">

                                <div class="small-section-tittle2">

                                    <h4>Work Type</h4>

                                </div>

                                <div class="select-job-items2">

                                    <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="true" CssClass="form-control w-100" OnSelectedIndexChanged="ddlType_SelectedIndexChanged" AppendDataBoundItems="true" DataTextField="Category" DataValueField="g_id">
                                        <asp:ListItem Value="0">Type</asp:ListItem>
                                        <asp:ListItem Value="1">Placement</asp:ListItem>
                                        <asp:ListItem Value="2">Internship</asp:ListItem>
                                    </asp:DropDownList>

                                </div>

                            </div>

                            <!-- select-Categories End -->

                        </div>

                        <!-- single two -->


                        <!-- single three -->

                        <div class="single-listing" style="margin-top: 20px">

                            <!-- select-Categories start -->

                            <div class="select-Categories pb-50">

                                <div class="small-section-tittle2">

                                    <h4>Difficulty Level</h4>

                                </div>

                                <div class="radiobuttoncontainer">

                                    <asp:RadioButtonList ID="rblLevel" runat="server" CssClass="radiobuttonlist" AutoPostBack="true"
                                        OnSelectedIndexChanged="rblLevel_SelectedIndexChanged" RepeatLayout="Flow">

                                        <asp:ListItem Value="0" Selected="True">Any</asp:ListItem>

                                        <asp:ListItem Value="1">Basic</asp:ListItem>

                                        <asp:ListItem Value="2">Moderate</asp:ListItem>

                                        <asp:ListItem Value="3">Advance</asp:ListItem>

                                    </asp:RadioButtonList>

                                </div>

                            </div>

                            <!-- select-Categories End -->

                            <%--                                <div class="mb-1">

                                    <asp:LinkButton ID="lbFilter" runat="server" CssClass="btn btn-sm" Width="100%"
                                        OnClick="lbFilter_Click">Filter</asp:LinkButton>

                                </div>--%>

                            <div class="mb-4">

                                <asp:LinkButton ID="lbReset" runat="server" CssClass="btn btn-sm" Width="100%"
                                    OnClick="lbReset_Click">Reset</asp:LinkButton>

                            </div>


                        </div>


                    </div>

                    <!-- Job Category Listing End -->

                </div>

                <!-- Right content -->

                <div class="col-xl-10 col-lg-9 col-md-8">

                    <!-- Featured_job_start -->

                    <section class="featured-job-area">

                        <div class="container">

                            <!-- Count of Job list Start -->

                            <div class="row">

                                <div class="col-lg-12">

                                    <div class="count-job mb-35">

                                        <asp:Label ID="lblCount" runat="server"></asp:Label>

                                    </div>

                                </div>

                            </div>

                            <!-- Count of Job list End -->

                            <!-- single-job-content -->

                            <asp:GridView ID="dtgInterview" runat="server" CssClass="table table-hover table-bordered" EmptyDataText="No record to display..!" AutoGenerateColumns="False" AllowPaging="True" PageSize="5" OnPageIndexChanging="dtgInterview_PageIndexChanging" DataKeyNames="g_id">
                                <Columns>

                                    <asp:BoundField DataField="Sr.No" HeaderText="Sr.No">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="Title" HeaderText="Field">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="Description" HeaderText="Description">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="Category" HeaderText="Type">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="Level" HeaderText="Level">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>


                                    <asp:TemplateField HeaderText="Guide">
                                        <ItemTemplate>
                                            <asp:Button ID="DownloadPDF" runat="server" Text="Download PDF" CommandArgument='<%# Eval("g_id") %>' OnClick="DownloadPDF_Click" />
                                            <asp:HiddenField ID="hdnJob_id" runat="server" Value='<%# Eval("g_id") %>' Visible="false" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                </Columns>
                                <HeaderStyle BackColor="#7200cf" ForeColor="White" />
                            </asp:GridView>


                        </div>

                    </section>

                    <!-- Featured_job_end -->

                </div>

            </div>

        </div>

    </div>

</asp:Content>
