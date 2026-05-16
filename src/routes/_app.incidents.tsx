import { createFileRoute } from "@tanstack/react-router";
import { Card, CardContent } from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import { Plus } from "lucide-react";
import { PageHeader } from "@/components/qhse/PageHeader";
import { SeverityBadge, StatusBadge } from "@/components/qhse/StatusBadge";
import { incidents } from "@/lib/qhse/mock-data";

export const Route = createFileRoute("/_app/incidents")({
  component: IncidentsPage,
});

function IncidentsPage() {
  return (
    <div className="space-y-6">
      <PageHeader
        title="Incidents"
        description="Suivi et gestion de tous les incidents déclarés sur les sites."
        actions={
          <Button>
            <Plus className="mr-2 h-4 w-4" /> Déclarer un incident
          </Button>
        }
      />

      <Card className="shadow-[var(--shadow-card)]">
        <CardContent className="p-0">
          <div className="overflow-x-auto">
            <table className="w-full text-sm">
              <thead className="bg-secondary/50">
                <tr className="text-left text-xs uppercase tracking-wider text-muted-foreground">
                  <th className="px-4 py-3 font-medium">ID</th>
                  <th className="px-4 py-3 font-medium">Titre</th>
                  <th className="px-4 py-3 font-medium">Site</th>
                  <th className="px-4 py-3 font-medium">Déclaré par</th>
                  <th className="px-4 py-3 font-medium">Sévérité</th>
                  <th className="px-4 py-3 font-medium">Statut</th>
                  <th className="px-4 py-3 font-medium">Date</th>
                </tr>
              </thead>
              <tbody>
                {incidents.map((inc) => (
                  <tr key={inc.id} className="border-t border-border hover:bg-secondary/30">
                    <td className="px-4 py-3 font-mono text-xs text-muted-foreground">{inc.id}</td>
                    <td className="px-4 py-3 font-medium text-foreground">{inc.title}</td>
                    <td className="px-4 py-3 text-muted-foreground">{inc.site}</td>
                    <td className="px-4 py-3 text-muted-foreground">{inc.reportedBy}</td>
                    <td className="px-4 py-3"><SeverityBadge severity={inc.severity} /></td>
                    <td className="px-4 py-3"><StatusBadge status={inc.status} /></td>
                    <td className="px-4 py-3 text-muted-foreground">{inc.date}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </CardContent>
      </Card>
    </div>
  );
}