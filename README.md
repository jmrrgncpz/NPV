# NPV
By [Jesmer Paz](mailto:paz.jesr@gmail.com)

## Installation
  1. Clone the project
  2. Client(npv-vue folder) Installation <br>
    2.1. Open a command line interface in the Client's root directory<br>
    2.2. Install dependencies with <code>npm i</code><br>
    2.3. Run client app with<code>npm run serve</code>
  3. Server(NPV folder) Installation<br>
    3.1. Execute the solution(NPV.sln)<br>
    3.2. Right click on the NPV project and select <strong>Clean</strong>.<br>
    3.3. Right click once again on the NPV project and select <strong>Rebuild</strong>.<br>
    3.4. Right click once more on the NPV project and select <strong>Set as StartUp Project</strong><br>
    3.5. Run the project(F5)
  4. Open a browser <i>(preferably Chrome)</i> and navigate to <strong>http://localhost:8080</strong>
  ## Software
    1. Visual Studio 2017
    2. VS Code 1.36.1
    3. NodeJS 8.12.0
    4. .NET Framework 4.6.1
## Discussion
The following technologies are used
  1. HTML
  2. Buefy (Bulma CSS Framework + Vue)
  3. VueJS
  4. Fontawesome Icons
  5. Axios (HTTP Client)
  6. Entity Framework 6<br>
<code>vue create</code> was run with <strong>Vue CLI 3.0</strong> to scaffold the client application.<br>
 Meanwhile, the server project was built with <strong>ASP.NET Web Application(.NET Framework)</strong> Empty Template including a WebAPI reference.
 
## Requirements
  ### User should be able to specify the following parameters
    1. Initial Value
    2. Lower Bound Discount Rate
    3. Upper Bound Discount Rate
    4. Discount Rate Increment
    5. Cashflow
    
  A space is handesomely provided for parameters. The user has the ability to add and remove cashflows.
  A reset button is provided aside from the essential -- <strong>Calculate button</strong>.
    
  ### NPV will be calculated at each Discount Rate -- from Lower Bound Discount Rate until Upper Bound Discount Rate
  The server uses a general purpose service that calls the methods from Calculation service class to do the actual calculation and saving of results.
    
  ### NPV values should be shown back to user
  Once the calculation is done, the results are sent back to the client and are displayed within the Results panel.
  The results are immediately injected to history items with javascript.
    
  ### Persist the results that users may be able to revisit previously ran results
  The project is using SQL Server to store the results of calculation.
  Entity Framework is used to save and retrieve the data from the database.
  Previously ran results are contained within the </strong>History</strong> panel of the application.
  These results are loaded once the app has started.
  The data shown within the <i>history items</i> are actually the parameters. The user can click on these to show the results.
  
  ### Add automated testing
  Unit testing framework used is the default testing for ASP.NET Framework. Tests are implemented in calculating the NPV.
