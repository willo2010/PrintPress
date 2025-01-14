using System.Data;
using Microsoft.Data.SqlClient;
using PrintPress.Controller.Data;
using PrintPress.Data;
using System.Net.Mail;
using PrintPress.Data.Enum;
using PrintPress.Controller.Enum;
using PrintPress.Data.Builder;

namespace PrintPress.Controller
{
    /// <summary>
    /// Commercial implementation of DataController singleton. Manages reading from and writing to the Commercial Database
    /// </summary>
    public class CommercialDataController : DataController<CommercialDataController>
    {
        // Define the table schema type for the DataController specification
        private CommercialDatabaseSchema _schema;
        protected override CommercialDatabaseSchema Tables { get { return _schema; } }

        /// <summary>
        /// Initialises DataController for the Commercial Database
        /// </summary>
        public override void Initialise()
        {
            _schema = new CommercialDatabaseSchema();
            Initialise("CommercialData");
        }

        /// <summary>
        /// Search the database for an employee matching the provided credentials
        /// </summary>
        /// <param name="mailAddress"> Employee email to search by as key </param>
        /// <param name="employee"> Out param of resulting EmployeeData </param>
        /// <param name="message"> Out param of possible error message in case of read failure </param>
        /// <returns> CommandReturnState indicating success of query </returns>
        public CommandReturnState GetEmployee(MailAddress mailAddress, out EmployeeData employee, out string message)
        {
            employee = new EmployeeData();
            message = "database error";
            
            if (!VerifyTable(Tables.Employee))
            {
                return CommandReturnState.FAILED;
            }

            SqlCommandData<EmployeeData> commandData = new SqlCommandData<EmployeeData>()
            {
                queryString = $"SELECT * FROM {Tables.Employee.Name} " +
                $"JOIN {Tables.Person.Name} ON {Tables.Employee.Name}.PersonID={Tables.Person.Name}.PersonID " +
                $"WHERE Email = @email",
                sqlParams = [new SqlParameter("@email", SqlDbType.NVarChar) { Value = mailAddress.Address }],
                readerFunc = reader =>
                {
                    int id = reader.GetInt32(1);
                    string jobTitle = reader.GetString(2);

                    GetPerson(id, out PersonalData person);

                    if (!GetClearance(id, out Department[] clearance))
                    {
                        return new EmployeeData();
                    }

                    return new EmployeeData(id, person, jobTitle,clearance);
                }
            };

            return GetSingleResult(commandData, out employee);
        }

        /// <summary>
        /// Search the database for an Person matching the provided credentials
        /// </summary>
        /// <param name="id"> PersonalData id int to search by as key </param>
        /// <param name="person"> Out param of resulting PersonalData </param>
        /// <returns> CommandReturnState indicating success of query </returns>
        private CommandReturnState GetPerson(int id, out PersonalData person)
        {
            SqlCommandData<PersonalData> commandData = new SqlCommandData<PersonalData>()
            {
                queryString = $"SELECT * FROM {Tables.Person.Name} WHERE PersonID = @personID",
                sqlParams = [new SqlParameter("@personID", SqlDbType.Int) { Value = id }],
                readerFunc = reader =>
                {
                    GetAddress(reader.GetInt32(1), out Address address);

                    return new PersonalData()
                    {
                        HomeAddress = address,
                        FirstNames = reader.GetString(2),
                        LastName = reader.GetString(3),
                        MailAddress = new MailAddress(reader.GetString(4)),
                        Phone = reader.GetString(5)
                    };
                }
            };

            return GetSingleResult(commandData, Tables.Person, out person);
        }

        /// <summary>
        /// Search the database for an Address matching the provided credentials
        /// </summary>
        /// <param name="id"> Address id int to search by as key </param>
        /// <param name="address"> Out param of resulting Address object </param>
        /// <returns> CommandReturnState indicating success of query </returns>
        private CommandReturnState GetAddress(int id, out Address address)
        {
            SqlCommandData<Address> commandData = new SqlCommandData<Address>()
            {
                queryString = $"SELECT * FROM {Tables.Address.Name} WHERE AddressID = @addressID",
                sqlParams = [new SqlParameter("@addressID", SqlDbType.Int) { Value = id }],
                readerFunc = reader =>
                {
                    return new Address()
                    {
                        HouseNameOrNumber = reader.GetString(1),
                        Road = reader.GetString(2),
                        City = reader.GetString(3),
                        County = reader.GetString(4),
                        Country = reader.GetString(5),
                        Postcode = reader.GetString(6),
                    };
                }
            };

            return GetSingleResult(commandData, Tables.Address, out address);
        }

        /// <summary>
        /// Search the database for Adverts matching the provided credentials
        /// </summary>
        /// <param name="employeeID"> Employee id int to search by as key </param>
        /// <param name="adverts"> Out param of resulting AdvertData[] </param>
        /// <param name="message"> Out param of possible error message, useful in case of read failure </param>
        /// <returns> CommandReturnState indicating success of query </returns>
        public CommandReturnState GetAdverts(int employeeID, out AdvertData[] adverts, out string message)
        {
            adverts = [];
            message = "database error";

            SqlCommandData<AdvertData> commandData = new SqlCommandData<AdvertData>()
            {
                queryString = $"SELECT * FROM {Tables.Advert.Name} " +
                $"JOIN {Tables.Content.Name} ON {Tables.Advert.Name}.ContentID = {Tables.Content.Name}.ContentID " +
                $"JOIN {Tables.Person.Name} ON {Tables.Person.Name}.PersonID = {Tables.Advert.Name}.ContactID " +
                $"JOIN {Tables.Address.Name} ON {Tables.Address.Name}.AddressID = {Tables.Person.Name}.AddressID " +
                $"WHERE {Tables.Content.Name}.EmployeeID = @employeeID",
                sqlParams = [new SqlParameter("@employeeID", SqlDbType.Int) { Value = employeeID }],
                readerFunc = reader =>
                {
                    AddressBuilder addressBuilder = new AddressBuilder();
                    addressBuilder.HouseNameOrNumber = reader.GetString("Number_Name");
                    addressBuilder.Road = reader.GetString("Road");
                    addressBuilder.City = reader.GetString("City");
                    addressBuilder.County = reader.GetString("County");
                    addressBuilder.Country = reader.GetString("Country");
                    addressBuilder.Postcode = reader.GetString("Postcode");

                    PersonalDataBuilder pdBuilder = new PersonalDataBuilder();
                    pdBuilder.Address = addressBuilder.ToAddress();
                    pdBuilder.FirstNames = addressBuilder.Road = reader.GetString("FirstNames");
                    pdBuilder.LastName = addressBuilder.Road = reader.GetString("LastName");
                    pdBuilder.Email = addressBuilder.Road = reader.GetString("Email");
                    pdBuilder.Phone = addressBuilder.Road = reader.GetString("PhoneNum");

                    AdvertBuilder advertBuilder = new AdvertBuilder();
                    advertBuilder.ContentID = reader.GetInt32("ContentID");
                    advertBuilder.Text = reader.GetString("Text");
                    advertBuilder.Title = reader.GetString("Title");
                    byte[] bytes = (byte[])reader["Image"];
                    advertBuilder.Notes = reader.GetString("Notes");
                    advertBuilder.Comments = reader.GetString("Comments");
                    advertBuilder.State = (ContentState)reader.GetInt32("State");
                    advertBuilder.LastSaved = reader.GetDateTime("LastSaved");
                    advertBuilder.Contact = pdBuilder.ToPersonalData();

                    return advertBuilder.ToAdvertData();
                }
            };

            return GetMultiResult(commandData, out adverts);
        }

        /// <summary>
        /// Search the database for Stories matching the provided credentials
        /// </summary>
        /// <param name="employeeID"> Employee id int to search by as key </param>
        /// <param name="stories"> Out param of resulting StoryData[] </param>
        /// <param name="message"> Out param of possible error message, useful in case of read failure </param>
        /// <returns> CommandReturnState indicating success of query </returns>
        public CommandReturnState GetStories(int employeeID, out StoryData[] stories, out string message)
        {
            stories = [];
            message = "database error";

            SqlCommandData<StoryData> commandData = new SqlCommandData<StoryData>()
            {
                queryString = $"SELECT * FROM {Tables.Story.Name} " +
                $"JOIN {Tables.Content.Name} ON {Tables.Story.Name}.ContentID = {Tables.Content.Name}.ContentID " +
                $"WHERE {Tables.Content.Name}.EmployeeID = @employeeID",
                sqlParams = [new SqlParameter("@employeeID", SqlDbType.Int) { Value = employeeID }],
                readerFunc = reader =>
                {
                    StoryBuilder builder = new StoryBuilder();

                    builder.ContentID = reader.GetInt32("ContentID");
                    builder.Text = reader.GetString("Text");
                    builder.Title = reader.GetString("Title");
                    byte[] bytes = (byte[])reader["Image"];
                    builder.Notes = reader.GetString("Notes");
                    builder.Comments = reader.GetString("Comments");
                    builder.State = (ContentState)reader.GetInt32("State");
                    builder.LastSaved = reader.GetDateTime("LastSaved");
                    builder.Source = reader.GetString("Source");

                    return builder.ToStoryData();
                }
            };

            return GetMultiResult(commandData, out stories);
        }

        /// <summary>
        /// Allocates and populates an EmployeeData into the data tables
        /// </summary>
        /// <param name="employee"> Employee data to be stored </param>
        /// <param name="autoID"> Out param giving the automatically assigned row ID </param>
        /// <returns> Boolean value indicating whether the write operation was successful etrt56</returns>
        public bool AddEmployee(EmployeeData employee, out int autoID)
        {
            autoID = -1;

            bool commandSuccess = AddPerson(employee.personalData, out int personId);
            if (!commandSuccess) return commandSuccess;

            commandSuccess = AddItemGetId(Tables.Employee,
                [personId.ToString(), employee.jobTitle],
                out autoID);

            if (commandSuccess)
            {
                AddClearance(autoID, employee.clearance);
            }

            return commandSuccess;
        }

        /// <summary>
        /// Allocates and populates PersonalData into the data tables
        /// </summary>
        /// <param name="person"> Personal data to be stored </param>
        /// <param name="autoID"> Out param giving the automatically assigned row ID </param>
        /// <returns> Boolean value indicating whether the write operation was successful </returns>
        private bool AddPerson(PersonalData person, out int autoID)
        {
            autoID = -1;

            switch (FindEmail(person.MailAddress, out int result))
            {
                case CommandReturnState.FOUND:
                    autoID = result;
                    return true;
                case CommandReturnState.FAILED:
                    return false;
                default:
                    break;
            }

            bool commandSuccess = AllocateAddress(person.HomeAddress, out int addressId);
            if (!commandSuccess)
            {
                return commandSuccess;
            }

            return AddItemGetId(Tables.Person, 
                [addressId.ToString(), person.FirstNames, person.LastName, person.MailAddress.Address, person.Phone],
                out autoID);
        }

        /// <summary>
        /// Allocates and populates a set of clearance, employee ID pairs into the Clearance table
        /// </summary>
        /// <param name="employeeID"> Associated employee ID int </param>
        /// <param name="clearance"> Associated department clearances </param>
        /// <returns> Boolean value indicating whether the write operation was successful </returns>
        private bool AddClearance(int employeeID, Department[] clearance)
        {
            bool failed = false;
            foreach (Department department in clearance)
            {
                bool readSuccess = AddItem(Tables.Clearance, [employeeID.ToString(), DataTools.DepartmentFromID(department).ToString()]);
                if (!readSuccess)
                {
                    failed = true;
                }
            }
            return !failed;
        }

        /// <summary>
        /// Updates ContentData in the database tables
        /// </summary>
        /// <param name="content"> Associated content data </param>
        /// <returns> Boolean value indicating whether the write operation was successful </returns>
        private bool UpdateContent(ContentData content)
        {
            SqlCommandData<object> commandData = new SqlCommandData<object>()
            {
                queryString = $"UPDATE {Tables.Content.Name} SET Text = @text, Title = @title, Image = @image, Notes = @notes, Comments = @comments, State = @state, LastSaved = @lastSaved " +
                $"WHERE ContentID = @contentID",
                sqlParams = [
                    new SqlParameter("@text", SqlDbType.VarChar) { Value = content.Text },
                    new SqlParameter("@title", SqlDbType.VarChar) { Value = content.Title },
                    new SqlParameter("@image", SqlDbType.VarBinary) { Value = DataTools.ImageToByteArray(content.Image) },
                    new SqlParameter("@notes", SqlDbType.VarChar) { Value = content.Notes },
                    new SqlParameter("@comments", SqlDbType.VarChar) { Value = content.Comments },
                    new SqlParameter("@state", SqlDbType.Int) { Value = (int)content.State },
                    new SqlParameter("@lastSaved", SqlDbType.DateTime) { Value = DateTime.Now },
                    new SqlParameter("@contentID", SqlDbType.Int) { Value = content.ContentID }
                ]
            };

            return ExecuteNonQuery(commandData);

        }

        /// <summary>
        /// Removes a content instance from the database
        /// </summary>
        /// <param name="contentID"> ID of content to be deleted </param>
        /// <returns> Boolean value indicating whether the write operation was successful </returns>
        public bool DeleteContent(int contentID)
        {
            SqlCommandData<object> commandData = new SqlCommandData<object>()
            {
                queryString = 
                $"DELETE FROM {Tables.Content.Name} " +
                $"WHERE ContentID = @contentID; " +
                $"DELETE FROM {Tables.Advert.Name} " +
                $"WHERE ContentID = @contentID; " +
                $"DELETE FROM {Tables.Story.Name} " +
                $"WHERE ContentID = @contentID; ",
                sqlParams = [
                    new SqlParameter("@contentID", SqlDbType.Int) { Value = contentID }
                ]
            };

            return ExecuteNonQuery(commandData);
        }

        /// <summary>
        /// Updates an existing story instance on the database, identified by story ID
        /// </summary>
        /// <param name="storyData"> The updated StoryData instance </param>
        /// <returns> Boolean value indicating whether the write operation was successful </returns>
        public bool UpdateStory(StoryData storyData)
        {
            if (!UpdateContent(storyData))
            {
                return false;
            }

            SqlCommandData<object> commandData = new SqlCommandData<object>()
            {
                queryString = $"UPDATE {Tables.Story.Name} SET Source = @source " +
                $"WHERE ContentID = @contentID",
                sqlParams = [
                    new SqlParameter("@source", SqlDbType.VarChar) { Value = storyData.Sources },
                    new SqlParameter("@contentID", SqlDbType.Int) { Value = storyData.ContentID }
                ]    
            };
            return ExecuteNonQuery(commandData);
        }

        /// <summary>
        /// Allocates and populates an Address into the data tables
        /// </summary>
        /// <param name="maybeAddress"> Address data to be stored </param>
        /// <param name="autoID"> Out param giving the automatically assigned row ID </param>
        /// <returns> Boolean value indicating whether the write operation was successful </returns>
        private bool AllocateAddress(Address? maybeAddress, out int autoID)
        {
            if (maybeAddress == null)
            {
                autoID = -1;
                return true;
            }
            Address address = (Address)maybeAddress;
            return AddItemGetId(Tables.Address,
                [address.HouseNameOrNumber, address.Road, address.City, address.County, address.Country, address.Postcode],
                out autoID);
        }

        /// <summary>
        /// Allocates and populates a Content instance in the data tables
        /// </summary>
        /// <param name="content"> Content instance to be stored </param>
        /// <param name="autoId"> Out param giving the automatically assigned row ID </param>
        /// <returns> Boolean value indicating whether the write operation was successful </returns>
        public bool AllocateContent(ContentData content, out int autoId)
        {
            autoId = -1;
            
            bool success = AddItemGetId(Tables.Content,
                [content.Assigned.Id.ToString(), content.Text, content.Title, DataTools.ImageToByteArray(content.Image), 
                content.Notes, content.Comments, ((int)content.State).ToString(), DateTime.Now],
                out int itemAutoId);
            if (success)
            {
                autoId = itemAutoId;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Allocates and populates a Story instance in the data tables
        /// </summary>
        /// <param name="employeeID"> ID of the story's assigned employee </param>
        /// <param name="autoId"> Out param giving the automatically assigned row ID </param>
        /// <returns> Boolean value indicating whether the write operation was successful </returns>
        public bool AllocateStory(int employeeID, out int autoId)
        {
            autoId = -1;

            StoryBuilder builder = new StoryBuilder();
            builder.Assigned = new EmployeeData(employeeID);
            StoryData story = builder.ToStoryData();

            if (!AllocateContent(story, out autoId))
            {
                return false;
            }
            return AddItem(Tables.Story,
                [autoId, story.Sources]);
        }

        /// <summary>
        /// Allocates and populates a Advert instance in the data tables
        /// </summary>
        /// <param name="employeeID"> ID of the advert's assigned employee </param>
        /// <param name="autoId"> Out param giving the automatically assigned row ID </param>
        /// <returns> Boolean value indicating whether the write operation was successful </returns>
        public bool AllocateAdvert(int employeeID, out int autoId)
        {
            autoId = -1;

            AdvertBuilder builder = new AdvertBuilder();
            builder.Assigned = new EmployeeData(employeeID);
            AdvertData advert = builder.ToAdvertData();

            if (!AllocateContent(advert, out int contentAutoId))
            {
                return false;
            }
            if (!AddPerson(advert.Contact, out int contactAutoId))
            {
                return false;
            }
            return AddItemGetId(Tables.Story,
                [contentAutoId, contactAutoId],
                out autoId);
        }

        /// <summary>
        /// Finds the PersonID for a given email from the Commercial database
        /// </summary>
        /// <param name="email"> The email address to check against </param>
        /// <param name="id"> Out param giving the PersonID value, if found </param>
        /// <returns> CommandReturnState indicating whether the email has been found </returns>
        private CommandReturnState FindEmail(MailAddress email, out int id)
        {
            id = -1;
            SqlCommandData<int> query = new SqlCommandData<int>(
                $"SELECT PersonID FROM {Tables.Person.Name} WHERE Email = @email",
                [new SqlParameter("@email", SqlDbType.VarChar) { Value = email.ToString() }],
                reader => reader.GetInt32(0));

            CommandReturnState crs = GetSingleResult(query, out int result);
            if (crs == CommandReturnState.FOUND)
            {
                id = result;
            }
            return crs;
        }

        /// <summary>
        /// Reads all clearance instances for the given EmployeeID into an array
        /// </summary>
        /// <param name="employeeID"> The employee ID to match against </param>
        /// <param name="clearance"> Our param of the Departments cleared for the matched ID </param>
        /// <returns> Boolean value indicating whether the write operation was successful </returns>
        private bool GetClearance(int employeeID, out Department[] clearance)
        {
            SqlCommandData<Department> commandData = new SqlCommandData<Department>()
            {
                queryString = $"SELECT DepartmentID FROM {Tables.Clearance.Name} " +
                $"WHERE {Tables.Clearance.Name}.EmployeeID = @employeeID",
                sqlParams = [new SqlParameter("@employeeID", SqlDbType.Int) { Value = employeeID }],
                readerFunc = reader =>
                {
                    return DataTools.DepartmentFromID(reader.GetInt32(0));
                }
            };

            CommandReturnState state = GetMultiResult(commandData, Tables.Clearance, out clearance);
            if (state == CommandReturnState.FOUND)
            {
                return true;
            }

            return false;
        }
    }
}
