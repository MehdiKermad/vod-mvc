using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace VideoOnDemand.Entity
{
    public class VODContextInitializer : DropCreateDatabaseIfModelChanges<VODContext>
    {

    } 
}