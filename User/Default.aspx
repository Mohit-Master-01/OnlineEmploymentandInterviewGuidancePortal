<%@ Page Title="" Language="C#" MasterPageFile="~/User/UserMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="OnlineJobPortal.User.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--    <style>
        .single-job-items {
            width: 100%;
            max-width: 600px; /* Adjust this width as needed */
            margin: 0 auto; /* Center the container */
            padding: 20px;
            box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);
            border-radius: 5px;
            background-color: #fff;
        }

        .job-items {
            display: flex;
            align-items: center;
        }

        .company-img img {
            margin-right: 15px;
        }

        .job-tittle {
            flex-grow: 1;
        }

        .items-link2 {
            text-align: right;
        }

    </style>--%>

    <main>

        <!-- slider Area Start-->
        <div class="slider-area ">
            <!-- Mobile Menu -->
            <div class="slider-active">
                <div class="single-slider slider-height d-flex align-items-center" data-background="../assets/img/hero/h1_hero.jpg">
                    <div class="container">
                        <div class="row">
                            <div class="col-xl-6 col-lg-9 col-md-10">
                                <div class="hero__caption">
                                    <h1>Find the most exciting startup jobs</h1>
                                </div>
                            </div>
                        </div>
                        <!-- Search Box -->
                        <div class="row">
                            <div class="col-xl-8">
                                <!-- form -->
                                <%--                             <form action="#" class="search-box">
                                 <div class="input-form">
                                     <input type="text" placeholder="Job Tittle or keyword">
                                 </div>
                                 <div class="select-form">
                                     <div class="select-itms">
                                         <select name="select" id="select1">
                                             <option value="">Location BD</option>
                                             <option value="">Location PK</option>
                                             <option value="">Location US</option>
                                             <option value="">Location UK</option>
                                         </select>
                                     </div>
                                 </div>
                                 <div class="search-form">
                                     <a href="#">Find job</a>
                                 </div>	
                             </form>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- slider Area End-->
        <!-- Our Services Start -->
        <div class="our-services section-pad-t30">
            <div class="container">
                <!-- Section Tittle -->
                <div class="row">
                    <div class="col-lg-12">
                        <div class="section-tittle text-center">
                            <span>FEATURED Job Packages</span>
                            <h2>Browse Top Categories </h2>
                        </div>
                    </div>
                </div>
                <div class="row d-flex justify-contnet-center">
                    <div class="col-xl-3 col-lg-3 col-md-4 col-sm-6">
                        <div class="single-services text-center mb-30">
                            <div class="services-ion">
                                <span class="flaticon-tour"></span>
                            </div>
                            <div class="services-cap">
                                <h5><a href="Job_listing.aspx">UI & UX Development</a></h5>
                                <%--<span>(653)</span>--%>
                            </div>
                        </div>
                    </div>
                    <div class="col-xl-3 col-lg-3 col-md-4 col-sm-6">
                        <div class="single-services text-center mb-30">
                            <div class="services-ion">
                                <span class="flaticon-cms"></span>
                            </div>
                            <div class="services-cap">
                                <h5><a href="Job_listing.aspx">Software Development</a></h5>
                                <%--<span>(658)</span>--%>
                            </div>
                        </div>
                    </div>
                    <div class="col-xl-3 col-lg-3 col-md-4 col-sm-6">
                        <div class="single-services text-center mb-30">
                            <div class="services-ion">
                                <span class="flaticon-report"></span>
                            </div>
                            <div class="services-cap">
                                <h5><a href="Job_listing.aspx">Data Analysis</a></h5>
                                <%--<span>(658)</span>--%>
                            </div>
                        </div>
                    </div>
                    <div class="col-xl-3 col-lg-3 col-md-4 col-sm-6">
                        <div class="single-services text-center mb-30">
                            <div class="services-ion">
                                <span class="flaticon-app"></span>
                            </div>
                            <div class="services-cap">
                                <h5><a href="Job_listing.aspx">Mobile Application</a></h5>
                                <%--<span>(658)</span>--%>
                            </div>
                        </div>
                    </div>
                    <div class="col-xl-3 col-lg-3 col-md-4 col-sm-6">
                        <div class="single-services text-center mb-30">
                            <div class="services-ion">
                                <span class="flaticon-helmet"></span>
                            </div>
                            <div class="services-cap">
                                <h5><a href="Job_listing.aspx">Web Construction</a></h5>
                                <%--<span>(658)</span>--%>
                            </div>
                        </div>
                    </div>
                    <div class="col-xl-3 col-lg-3 col-md-4 col-sm-6">
                        <div class="single-services text-center mb-30">
                            <div class="services-ion">
                                <span class="flaticon-high-tech"></span>
                            </div>
                            <div class="services-cap">
                                <h5><a href="Job_listing.aspx">Information Technology</a></h5>
                                <%--<span>(658)</span>--%>
                            </div>
                        </div>
                    </div>
                    <div class="col-xl-3 col-lg-3 col-md-4 col-sm-6">
                        <div class="single-services text-center mb-30">
                            <div class="services-ion">
                                <span class="flaticon-curriculum-vitae"></span>
                            </div>
                            <div class="services-cap">
                                <h5><a href="Job_listing.aspx">Big Data Analysis</a></h5>
                                <%--<span>(658)</span>--%>
                            </div>
                        </div>
                    </div>
                    <div class="col-xl-3 col-lg-3 col-md-4 col-sm-6">
                        <div class="single-services text-center mb-30">
                            <div class="services-ion">
                                <span class="flaticon-content"></span>
                            </div>
                            <div class="services-cap">
                                <h5><a href="Job_listing.aspx">Algorithm & Flowchart Handler</a></h5>
                                <%--<span>(658)</span>--%>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- More Btn -->
                <!-- Section Button -->
                <div class="row">
                    <div class="col-lg-12">
                        <div class="browse-btn2 text-center mt-50">
                            <a href="Job_listing.aspx" class="border-btn2">Browse All Jobs</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- Our Services End -->
        <!-- Online CV Area Start -->
        <div class="online-cv cv-bg section-overly pt-90 pb-120" data-background="../assets/img/gallery/cv_bg.jpg">
            <div class="container">
                <div class="row justify-content-center">
                    <div class="col-xl-10">
                        <div class="cv-caption text-center">
                            <p class="pera2">Make a Difference with Your Resume!</p>
                            <a href="ResumeBuild.aspx" class="border-btn2 border-btn4">Upload your cv</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- Online CV Area End-->
        <!-- Featured_job_start -->
        <section class="featured-job-area feature-padding">
            <div class="container">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="section-tittle text-center">
                            <span>Recent Job</span>
                            <h2>Featured Jobs</h2>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row justify-content-center">
                <div class="col-xl-10">
                    <div class="single-job-items mb-30">
                        <div class="row justify-content-center">
                            <div class="job-items">
                                <asp:DataList ID="dlJobList" runat="server" Height="753px" RepeatColumns="1" Width="1215px" AlternatingItemStyle-HorizontalAlign="Center">

                                    <ItemTemplate>

                                        <div class="single-job-items mb-30">

                                            <div class="job-items">
                                                <div class="company-img">

                                                    <a href="Job_details.aspx?id=<%# Eval("Job_id") %>">
                                                    <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("CompanyImage") %>' Width="70px" Height="70px"/>
                                                    </a>

                                                </div>

                                                <div class="job-tittle job-tittle2">

                                                    <a href="Job_details.aspx?id=<%# Eval("Job_id") %>">

                                                        <h5><%# Eval("Title") %></h5>

                                                    </a>

                                                    <ul>

                                                        <li><%# Eval("CompanyName") %></li>

                                                        <li><i class="fas fa-map-marker-alt"></i><%# Eval("State") %>, <%# Eval("Country") %></li>

                                                        <li><%# Eval("Salary") %></li>

                                                    </ul>

                                                </div>

                                            </div>

                                            <div class="items-link items-link2 f-right">

                                                <a href="Job_details.aspx?id=<%# Eval("Job_id") %>"><%# Eval("JobType") %></a>

                                                <span class="text-secondary">

                                                    <i class="fas fa-clock pr-1"></i>

                                                    <%# RelativeDate(Convert.ToDateTime(Eval("CreateDate"))) %>

                                                </span>

                                            </div>

                                        </div>

                                    </ItemTemplate>

                                </asp:DataList>

                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </section>
        <!-- Featured_job_end -->
        <!-- How  Apply Process Start-->
        <div class="apply-process-area apply-bg pt-150 pb-150" data-background="../assets/img/gallery/how-applybg.png">
            <div class="container">
                <!-- Section Tittle -->
                <div class="row">
                    <div class="col-lg-12">
                        <div class="section-tittle white-text text-center">
                            <span>Apply process</span>
                            <h2>How it works</h2>
                        </div>
                    </div>
                </div>
                <!-- Apply Process Caption -->
                <div class="row">
                    <div class="col-lg-4 col-md-6">
                        <div class="single-process text-center mb-30">
                            <div class="process-ion">
                                <span class="flaticon-search"></span>
                            </div>
                            <div class="process-cap">
                                <h5>1. Search a job</h5>
                                <p>Discover your next IT career. Browse top tech jobs and find the perfect match for your skills and ambitions.</p>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-6">
                        <div class="single-process text-center mb-30">
                            <div class="process-ion">
                                <span class="flaticon-curriculum-vitae"></span>
                            </div>
                            <div class="process-cap">
                                <h5>2. Apply for job</h5>
                                <p>Apply for your dream IT job. Take the next step in your tech career and join a leading company today.</p>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-6">
                        <div class="single-process text-center mb-30">
                            <div class="process-ion">
                                <span class="flaticon-tour"></span>
                            </div>
                            <div class="process-cap">
                                <h5>3. Get your job</h5>
                                <p>Secure your next opportunity. Explore, apply, and step into your dream job with confidence.</p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- How  Apply Process End-->
        <!-- Testimonial Start -->
        <div class="testimonial-area testimonial-padding">
            <div class="container">
                <!-- Testimonial contents -->
                <div class="row d-flex justify-content-center">
                    <div class="col-xl-8 col-lg-8 col-md-10">
                        <div class="h1-testimonial-active dot-style">
                            <!-- Single Testimonial -->
                            <div class="single-testimonial text-center">
                                <!-- Testimonial Content -->
                                <div class="testimonial-caption ">
                                    <!-- founder -->
                                    <div class="testimonial-founder  ">
                                        <div class="founder-img mb-30">
                                            <img src="../assets/img/testmonial/testimonial-founder.png" alt="">
                                            <span>John S.</span>
                                            <p>Software Developer</p>
                                        </div>
                                    </div>
                                    <div class="testimonial-top-cap">
                                        <p>"Thanks to CrackIT, I found the perfect role that aligns with my skills and career goals. The platform is user-friendly, and I was able to apply to multiple jobs with ease. The guidance provided throughout the process was invaluable. Highly recommended!"</p>
                                    </div>
                                </div>
                            </div>
                            <!-- Single Testimonial -->
                            <div class="single-testimonial text-center">
                                <!-- Testimonial Content -->
                                <div class="testimonial-caption ">
                                    <!-- founder -->
                                    <div class="testimonial-founder  ">
                                        <div class="founder-img mb-30">
                                            <img src="../assets/img/testmonial/testimonial-founder.png" alt="">
                                            <span>Emily R.</span>
                                            <p>IT Project Manager</p>
                                        </div>
                                    </div>
                                    <div class="testimonial-top-cap">
                                        <p>"I was impressed by the variety of job listings available on CrackIT. The site made it simple to search and filter jobs that matched my experience. Within a few weeks, I landed an interview and secured a position at a top IT company."</p>
                                    </div>
                                </div>
                            </div>
                            <!-- Single Testimonial -->
                            <div class="single-testimonial text-center">
                                <!-- Testimonial Content -->
                                <div class="testimonial-caption ">
                                    <!-- founder -->
                                    <div class="testimonial-founder  ">
                                        <div class="founder-img mb-30">
                                            <img src="../assets/img/testmonial/testimonial-founder.png" alt="">
                                            <span>Michael T.</span>
                                            <p>Network Engineer</p>
                                        </div>
                                    </div>
                                    <div class="testimonial-top-cap">
                                        <p>"The job search process can be overwhelming, but CrackIT made it straightforward and efficient. I appreciated the personalized job recommendations and the timely updates. This site helped me find a job that I’m truly passionate about."</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- Testimonial End -->
        <!-- Support Company Start-->
        <div class="support-company-area support-padding fix">
            <div class="container">
                <div class="row align-items-center">
                    <div class="col-xl-6 col-lg-6">
                        <div class="right-caption">
                            <!-- Section Tittle -->
                            <div class="section-tittle section-tittle2">
                                <span>What we are doing</span>
                                <h2>24k Talented people are getting Jobs</h2>
                            </div>
                            <div class="support-caption">
                                <p class="pera-top">At CrackIT, we are dedicated to connecting IT professionals with their dream jobs. By curating a wide range of opportunities across the tech industry, we make it easier for job seekers to find roles that match their skills and aspirations. Our mission is to bridge the gap between talent and opportunity, helping individuals advance in their careers and achieve their professional goals.</p>
                                <p>We understand that searching for a job can be a daunting task. That's why we've built a platform that simplifies the process, allowing you to focus on what matters most—finding the right job.</p>
                                <a href="../Admin/NewJob.aspx" class="btn post-btn">Post a job</a>
                            </div>
                        </div>
                    </div>
                    <div class="col-xl-6 col-lg-6">
                        <div class="support-location-img">
                            <img src="../assets/img/service/support-img.jpg" alt="">
                            <div class="support-img-cap text-center">
                                <p>Since</p>
                                <span>1994</span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- Support Company End-->
        <!-- Blog Area Start -->
        <div class="home-blog-area blog-h-padding">
            <div class="container">
                <!-- Section Tittle -->
                <div class="row">
                    <div class="col-lg-12">
                        <div class="section-tittle text-center">
                            <span>Our latest blog</span>
                            <h2>Our recent news</h2>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xl-6 col-lg-6 col-md-6">
                        <div class="home-blog-single mb-30">
                            <div class="blog-img-cap">
                                <div class="blog-img">
                                    <img src="../assets/img/blog/home-blog1.jpg" alt="">
                                    <!-- Blog date -->
                                    <div class="blog-date text-center">
                                        <span>24</span>
                                        <p>Now</p>
                                    </div>
                                </div>
                                <div class="blog-cap">
                                    <p>|   Properties</p>
                                    <h3>Footprints in Time is perfect House in Kurashiki</h3>
                                    <a href="https://www.architectural-review.com/awards/ar-house/footprints-in-time-house-in-kamitomii-kurashiki-japan-by-general-design-co" class="more-btn">Read more »</a>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-xl-6 col-lg-6 col-md-6">
                        <div class="home-blog-single mb-30">
                            <div class="blog-img-cap">
                                <div class="blog-img">
                                    <img src="../assets/img/blog/home-blog2.jpg" alt="">
                                    <!-- Blog date -->
                                    <div class="blog-date text-center">
                                        <span>24</span>
                                        <p>Now</p>
                                    </div>
                                </div>
                                <div class="blog-cap">
                                    <p>|   Properties</p>
                                    <h3>Why are Business Analysts Vital for any Project’s Success?</h3>
                                    <a href="https://www.intelegain.com/why-are-business-analysts-vital-for-any-projects-success/" class="more-btn">Read more »</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- Blog Area End -->

    </main>

</asp:Content>
