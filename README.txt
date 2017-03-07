Synopsis:-
This application is developed to preview & edit HTML files. User of this application will have to only configure the path and name of the html files and application will read this html file and display how it will look. It is using CKEditor control to handle all the html editing functionality. Once the HTML file is get uploaded into the ckeditor control of web page, user can also make changes in this html file and preview its contents. It will be very helpful for a project in which html editing feature is required and can very easily integrated into those applications.

Prerequisites:-
1.	Microsoft Visual Studio-2013
2.	CKEditor 4

Installation:-
Installing of this application is an easy task and need to just follow these simple steps:

1. **Download** the latest version from the path: https://github.com/AshishCKH/CKEditor_Trigyn 

2. **Extract** (decompress) the downloaded file into the root of your website.

Usage:-

1. Once the application is decompressed it will show the entire solution directory.

2. Use the Microsoft Visual Studio-2013 and open the solution.

3. In the web.config file of the solution configure the database connection string.

4. Bydefault application is using the State Server session state and if you wish to you InProc session state than change the mode to Inproc for session state.

5. Solution is using stored procedure "XMLFilesDetailGetByID" to fetch details i.e. html file name and file path which is get configured in the database so as per requirement need to add one table in the database for filename and filepath and stored procedure will fetch details from this table based on the id parameter

6. After that will use stream reader to read the html file and one by one this content will get added into CKEditor HTML Control. Once this details get added into this control user can preview content of this html file to see how it will look in browser and also can make changes into html text either by using the toolbar options provided by this control or can directly go to the source to see the html and change it there.  

License:-
This project is licensed under the LGPL License - see the LICENSE.txt file for details.

Contributing :-

Please read CONTRIBUTING.txt for details on our code of conduct, and the process for submitting pull requests to us.
