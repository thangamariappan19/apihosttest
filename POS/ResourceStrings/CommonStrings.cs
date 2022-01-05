using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceStrings
{
    public static class CommonStrings
    {
        public static string RetrievalErrorMessage = "Error retrieving {} Data";
        public static string BLLRetrievalErrorMessage = "Error retrieving {} Data in the BLL.";
        public static string UpdateErrorMessage = "Error updating {} Data";
        public static string DeleteErrorMessage = "Error Deleting {} Data";
        public static string NoChangeToOriginalDataMessage = "No change to {} Data. Update cancelled.";
        public static string UpdateErrorDueToNewerVersionInDB = "Error updating {} Data. Newer version found in Database.";
        public static string SuccessfulSaveMessage = "{}  Saved Successfully.";
        public static string AlreadyLogedIN = "{} Already Loged IN.";
        public static string SuccessfulUpdateMessage = "{} Updated Successfully.";
        public static string SuccessfulDeleteMessage = "{} Deleted Successfully.";
        public static string SaveErrorMessage = "{} Save Process Failed.";
        public static string ItIsInRelationdhip = "{} successfully updated But The Delete Process is failed.";
        public static string AlreadyExists = "{} Already Exists.";
        public static string NoRecordFound = "No {} Record Found !";
        public static string UploadRecordMismatch = "Records Mismatch Check Your {} Uploaded Records";
        public static string PasswordNotMatch = "Password Does not match !";
        public static string NoAction = "No Action in {} Data.";
        public static string DownloadError = "DownLoad {} File Operation Cancelled.Records Not Found";
        public static string ServerNotResponse = "{}";
        public static string DuplicateValuesMessage = "Please check the data for duplicate {} values. Process aborted.";
        public static string DoesNotExists = "{} does not Exist";
        public static string PassGenerationErrorMessage = "Error in {} Pass No Generation.";
        public static string GeneralBillingError = "Error processing Billing data.";
        public static string NotFound = "{} Not Found.";
        public static string RecordMismatch = "{} .";
        public static string ClientSyncFailed = "{0} Record Sync Failed to server {1}";
    }
}
