import { createFileRoute } from "@tanstack/react-router";
import { Card, CardContent } from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import { Plus } from "lucide-react";
import { PageHeader } from "@/components/qhse/PageHeader";
import { SeverityBadge, StatusBadge } from "@/components/qhse/StatusBadge";
import { nonConformities } from "@/lib/qhse/mock-data";

export const Route = createFileRoute("/_app/nonconformities")({
  component: NonConformitiesPage,
});

function NonConformitiesPage() {
  return (
    <div className="space-y-6">
      <PageHeader
        title="Non-conformités"
        description="Détection, traitement et clôture des non-conformités."
        actions={<Button><Plus className="mr-2 h-4 w-4" /> Nouvelle NC</Button>}
      />
      <Card className="shadow-[var(--shadow-card)]">
        <CardContent className="p-0">
          <div className="overflow-x-auto">
            <table className="w-full text-sm">
              <thead className="bg-secondary/50">
                <tr className="text-left text-xs uppercase tracking-wider text-muted-foreground">
                  <th className="px-4 py-3 font-medium">ID</th>
                  <th className="px-4 py-3 font-medium">Titre</th>
                  <th className="px-4 py-3 font-medium">Source</th>
                  <th className="px-4 py-3 font-medium">Responsable</th>
                  <th className="px-4 py-3 font-medium">Échéance</th>
                  <th className="px-4 py-3 font-medium">Sévérité</th>
                  <th className="px-4 py-3 font-medium">Statut</th>
                </tr>
              </thead>
              <tbody>
                {nonConformities.map((nc) => (
                  <tr key={nc.id} className="border-t border-border hover:bg-secondary/30">
                    <td className="px-4 py-3 font-mono text-xs text-muted-foreground">{nc.id}</td>
                    <td className="px-4 py-3 font-medium text-foreground">{nc.title}</td>
                    <td className="px-4 py-3 font-mono text-xs text-muted-foreground">{nc.source}</td>
                    <td className="px-4 py-3 text-muted-foreground">{nc.owner}</td>
                    <td className="px-4 py-3 text-muted-foreground">{nc.dueDate}</td>
                    <td className="px-4 py-3"><SeverityBadge severity={nc.severity} /></td>
                    <td className="px-4 py-3"><StatusBadge status={nc.status} /></td>
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