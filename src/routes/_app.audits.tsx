import { createFileRoute } from "@tanstack/react-router";
import { Card, CardContent } from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import { Badge } from "@/components/ui/badge";
import { Plus } from "lucide-react";
import { PageHeader } from "@/components/qhse/PageHeader";
import { audits } from "@/lib/qhse/mock-data";

export const Route = createFileRoute("/_app/audits")({
  component: AuditsPage,
});

const statusMap = {
  planned: { label: "Planifié", className: "bg-primary/15 text-primary border-primary/30" },
  ongoing: { label: "En cours", className: "bg-warning/20 text-warning-foreground border-warning/40" },
  completed: { label: "Terminé", className: "bg-success/15 text-success border-success/30" },
};

function AuditsPage() {
  return (
    <div className="space-y-6">
      <PageHeader
        title="Audits"
        description="Planification, exécution et résultats des audits internes et externes."
        actions={
          <Button>
            <Plus className="mr-2 h-4 w-4" /> Planifier un audit
          </Button>
        }
      />
      <div className="grid grid-cols-1 gap-4 md:grid-cols-2 xl:grid-cols-3">
        {audits.map((audit) => {
          const s = statusMap[audit.status];
          return (
            <Card key={audit.id} className="shadow-[var(--shadow-card)]">
              <CardContent className="p-5">
                <div className="flex items-start justify-between">
                  <div>
                    <p className="font-mono text-xs text-muted-foreground">{audit.id}</p>
                    <h3 className="mt-1 font-semibold text-foreground">{audit.reference}</h3>
                  </div>
                  <Badge variant="outline" className={s.className}>{s.label}</Badge>
                </div>
                <p className="mt-3 text-sm text-muted-foreground">{audit.scope}</p>
                <div className="mt-4 grid grid-cols-2 gap-3 border-t border-border pt-4 text-sm">
                  <div>
                    <p className="text-xs text-muted-foreground">Auditeur</p>
                    <p className="font-medium text-foreground">{audit.auditor}</p>
                  </div>
                  <div>
                    <p className="text-xs text-muted-foreground">Date</p>
                    <p className="font-medium text-foreground">{audit.date}</p>
                  </div>
                </div>
                {audit.status === "completed" && (
                  <div className="mt-4 rounded-md bg-secondary p-3">
                    <div className="flex items-center justify-between">
                      <span className="text-xs text-muted-foreground">Score de conformité</span>
                      <span className="text-lg font-semibold text-foreground">{audit.score}%</span>
                    </div>
                    <div className="mt-2 h-2 overflow-hidden rounded-full bg-background">
                      <div className="h-full rounded-full bg-gradient-to-r from-primary to-[var(--primary-glow)]" style={{ width: `${audit.score}%` }} />
                    </div>
                  </div>
                )}
              </CardContent>
            </Card>
          );
        })}
      </div>
    </div>
  );
}