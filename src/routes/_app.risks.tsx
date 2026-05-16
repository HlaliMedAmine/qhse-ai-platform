import { createFileRoute } from "@tanstack/react-router";
import { Fragment } from "react";
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import { Plus } from "lucide-react";
import { PageHeader } from "@/components/qhse/PageHeader";
import { risks } from "@/lib/qhse/mock-data";

export const Route = createFileRoute("/_app/risks")({
  component: RisksPage,
});

function riskColor(score: number) {
  if (score >= 16) return "bg-destructive text-destructive-foreground";
  if (score >= 10) return "bg-warning text-warning-foreground";
  if (score >= 5) return "bg-primary/70 text-primary-foreground";
  return "bg-success/70 text-success-foreground";
}

function riskLabel(score: number) {
  if (score >= 16) return "Critique";
  if (score >= 10) return "Élevé";
  if (score >= 5) return "Modéré";
  return "Faible";
}

function RisksPage() {
  return (
    <div className="space-y-6">
      <PageHeader
        title="Analyse de risques"
        description="Cartographie des risques selon la matrice Probabilité × Impact."
        actions={<Button><Plus className="mr-2 h-4 w-4" /> Ajouter un risque</Button>}
      />

      <Card className="shadow-[var(--shadow-card)]">
        <CardHeader><CardTitle className="text-base">Matrice des risques</CardTitle></CardHeader>
        <CardContent>
          <div className="grid grid-cols-6 gap-1 text-xs">
            <div></div>
            {[1, 2, 3, 4, 5].map((i) => (
              <div key={`h-${i}`} className="text-center font-medium text-muted-foreground">Impact {i}</div>
            ))}
            {[5, 4, 3, 2, 1].map((p) => (
              <Fragment key={`row-${p}`}>
                <div className="flex items-center justify-end pr-2 font-medium text-muted-foreground">Prob. {p}</div>
                {[1, 2, 3, 4, 5].map((i) => {
                  const score = p * i;
                  const cellRisks = risks.filter((r) => r.likelihood === p && r.impact === i);
                  return (
                    <div key={`c-${p}-${i}`} className={`flex h-16 flex-col items-center justify-center rounded-md p-1 ${riskColor(score)}`}>
                      <span className="text-sm font-semibold">{score}</span>
                      {cellRisks.length > 0 && (
                        <span className="text-[10px] opacity-90">{cellRisks.length} risque{cellRisks.length > 1 ? "s" : ""}</span>
                      )}
                    </div>
                  );
                })}
              </Fragment>
            ))}
          </div>
        </CardContent>
      </Card>

      <Card className="shadow-[var(--shadow-card)]">
        <CardContent className="p-0">
          <div className="overflow-x-auto">
            <table className="w-full text-sm">
              <thead className="bg-secondary/50">
                <tr className="text-left text-xs uppercase tracking-wider text-muted-foreground">
                  <th className="px-4 py-3 font-medium">ID</th>
                  <th className="px-4 py-3 font-medium">Danger</th>
                  <th className="px-4 py-3 font-medium">Catégorie</th>
                  <th className="px-4 py-3 font-medium">P × I</th>
                  <th className="px-4 py-3 font-medium">Niveau</th>
                  <th className="px-4 py-3 font-medium">Mesure d'atténuation</th>
                </tr>
              </thead>
              <tbody>
                {risks.map((r) => {
                  const score = r.likelihood * r.impact;
                  return (
                    <tr key={r.id} className="border-t border-border hover:bg-secondary/30">
                      <td className="px-4 py-3 font-mono text-xs text-muted-foreground">{r.id}</td>
                      <td className="px-4 py-3 font-medium text-foreground">{r.hazard}</td>
                      <td className="px-4 py-3 text-muted-foreground">{r.category}</td>
                      <td className="px-4 py-3 font-mono text-xs text-muted-foreground">{r.likelihood} × {r.impact} = {score}</td>
                      <td className="px-4 py-3"><span className={`rounded-md px-2 py-1 text-xs font-medium ${riskColor(score)}`}>{riskLabel(score)}</span></td>
                      <td className="px-4 py-3 text-muted-foreground">{r.mitigation}</td>
                    </tr>
                  );
                })}
              </tbody>
            </table>
          </div>
        </CardContent>
      </Card>
    </div>
  );
}