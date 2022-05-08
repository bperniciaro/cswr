<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Responsive.master" AutoEventWireup="true" CodeFile="Students.aspx.cs" Inherits="temp_2_41_Students" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" Runat="Server">


   <script type="text/javascript">  
            function getStudents() {  
                $.getJSON("api/College",  
                    function(data) {  
                        $('#stud').empty(); // Clear table body.  
  
                        // Loop through the list of students.  
                        $.each(data, function(key, val) {  
                            // Add a table row for the student.  
                            var row = '<tr><td>' + val.StudName +  
                                '</td><td>' + val.StudAddress + '</td><td>' +  
                                val.StudMONO + '</td><td>' + val.StudCourse +  
                                '</td></tr>';  
                            $("#stud").append(row);  
  
                        });  
                    });  
            }  
            $(document).ready(getStudents);  
   </script>  

      <h2> Here is the Students Record</h2>  
            <table>  
                <thead>  
                    <tr>  
                        <th>StudName</th>  
                        <th>StudAddress</th>  
                        <th>StudMONO</th>  
                        <th>StudCourse</th>  
                    </tr>  
                </thead>  
                <tbody id="stud">  
                </tbody>  
            </table>  


</asp:Content>

