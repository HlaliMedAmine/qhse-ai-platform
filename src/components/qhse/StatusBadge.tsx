import { Badge } from "@/components/ui/badge";
import type { Severity, Status } from "@/lib/qhse/mock-data";

const severityStyles: Record<Severity, string> = {
  low: "bg-success/15 text-success border-success/30",
  medium: "bg-primary/15 text-primary border-primary/30",
  high: "bg-warning/20 text-warning-foreground border-warning/40",
  critical: "bg-destructive/15 text-destructive border-destructive/30",
};

const severityLabels: Record<Severity, string> = {
  low: "Faible",
  medium: "Moyenne",
  high: "Élevée",
  critical: "Critique",
};

const statusStyles: Record<Status, string> = {
  open: "bg-destructive/10 text-destructive border-destructive/30",
  in_progress: "bg-warning/20 text-warning-foreground border-warning/40",
  resolved: "bg-success/15 text-success border-success/30",
  closed: "bg-muted text-muted-foreground border-border",
};

const statusLabels: Record<Status, string> = {
  open: "Ouvert",
  in_progress: "En cours",
  resolved: "Résolu",
  closed: "Clôturé",
};

export function SeverityBadge({ severity }: { severity: Severity }) {
  return (
    <Badge variant="outline" className={severityStyles[severity]}>
      {severityLabels[severity]}
    </Badge>
  );
}

export function StatusBadge({ status }: { status: Status }) {
  return (
    <Badge variant="outline" className={statusStyles[status]}>
      {statusLabels[status]}
    </Badge>
  );
}