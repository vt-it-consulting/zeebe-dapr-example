﻿using System.ComponentModel.DataAnnotations;

namespace Zeebe.Worker.Models.Command;

public record ResolveIncident([Required] long IncidentKey);