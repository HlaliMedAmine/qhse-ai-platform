import { createFileRoute } from "@tanstack/react-router";
import { Card, CardContent } from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import { Upload, FileText, Download } from "lucide-react";
import { PageHeader } from "@/components/qhse/PageHeader";
import { reports } from "@/lib/qhse/mock-data";

export const Route = createFileRoute("/_app/reports")({
  component: ReportsPage,
});

function ReportsPage() {
  return (
    <div className="space-y-6">
      <PageHeader
        title="Rapports"
        description="Bibliothèque centralisée de tous les rapports QHSE."
        actions={<Button><Upload className="mr-2 h-4 w-4" /> Téléverser un rapport</Button>}
      />
      <div className="grid grid-cols-1 gap-4 md:grid-cols-2 lg:grid-cols-3">
        {reports.map((r) => (
          <Card key={r.id} className="shadow-[var(--shadow-card)] transition-shadow hover:shadow-[var(--shadow-elevated)]">
            <CardContent className="p-5">
              <div className="flex items-start gap-4">
                <div className="flex h-12 w-12 shrink-0 items-center justify-center rounded-lg bg-primary/10 text-primary">
                  <FileText className="h-6 w-6" />
                </div>
                <div className="min-w-0 flex-1">
                  <p className="font-mono text-xs text-muted-foreground">{r.id}</p>
                  <h3 className="mt-1 truncate font-semibold text-foreground">{r.name}</h3>
                  <p className="mt-1 text-xs text-muted-foreground">{r.type} · {(r.sizeKb / 1024).toFixed(2)} MB · {r.uploadedAt}</p>
                  <p className="mt-2 text-xs text-muted-foreground">Par {r.uploadedBy}</p>
                </div>
              </div>
              <div className="mt-4 flex gap-2">
                <Button variant="outline" size="sm" className="flex-1"><Download className="mr-2 h-3.5 w-3.5" /> Télécharger</Button>
                <Button size="sm" className="flex-1">Analyser IA</Button>
              </div>
            </CardContent>
          </Card>
        ))}
      </div>
    </div>
  );
}