import { createFileRoute } from "@tanstack/react-router";
import { useState } from "react";
import { Card, CardContent } from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import { Textarea } from "@/components/ui/textarea";
import { Sparkles, Send, Bot, User } from "lucide-react";
import { PageHeader } from "@/components/qhse/PageHeader";
import { apiUrl } from "@/lib/api";

export const Route = createFileRoute("/_app/ai-assistant")({
  component: AssistantPage,
});

const AI_ANALYZE_URL = apiUrl("/api/ai/analyze");

interface AiAnalyzeResponse {
  summary: string;
  riskLevel: string;
  correctiveActions: string[];
  recommendations: string[];
}

interface Msg {
  role: "user" | "assistant";
  content?: string;
  analysis?: AiAnalyzeResponse;
  isError?: boolean;
}

const initialMessages: Msg[] = [
  {
    role: "assistant",
    content:
      "Bonjour Elise. Je suis votre assistant QHSE. Je peux analyser un rapport, classifier un risque, ou proposer des actions correctives. Que voulez-vous faire ?",
  },
];

const suggestions = [
  "Analyser le rapport mensuel HSE - Avril 2025",
  "Classifier le risque INC-2041 (fuite chimique)",
  "Proposer des actions correctives pour NC-511",
  "Synthetiser les audits ISO du dernier trimestre",
];

function AssistantPage() {
  const [messages, setMessages] = useState<Msg[]>(initialMessages);
  const [input, setInput] = useState("");
  const [isLoading, setIsLoading] = useState(false);

  const send = async (text: string) => {
    const trimmedText = text.trim();
    if (!trimmedText || isLoading) return;

    setMessages((m) => [...m, { role: "user", content: trimmedText }]);
    setInput("");
    setIsLoading(true);

    try {
      const response = await fetch(AI_ANALYZE_URL, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          text: trimmedText,
          sourceType: "chat",
        }),
      });

      if (!response.ok) {
        throw new Error(`Backend returned ${response.status}`);
      }

      const analysis = (await response.json()) as AiAnalyzeResponse;
      setMessages((m) => [...m, { role: "assistant", analysis }]);
    } catch {
      setMessages((m) => [
        ...m,
        {
          role: "assistant",
          isError: true,
          content:
            "Impossible de contacter l'API d'analyse QHSE. Verifiez que le backend Azure est disponible et que la configuration CORS autorise cette application.",
        },
      ]);
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <div className="space-y-6">
      <PageHeader title="Assistant IA" description="Analyse de rapports, classification de risques et recommandations propulsees par IA." />
      <div className="grid grid-cols-1 gap-6 lg:grid-cols-[1fr_280px]">
        <Card className="flex h-[600px] flex-col shadow-[var(--shadow-card)]">
          <CardContent className="flex flex-1 flex-col gap-4 overflow-y-auto p-6">
            {messages.map((m, i) => (
              <div key={i} className={`flex gap-3 ${m.role === "user" ? "flex-row-reverse" : ""}`}>
                <div className={`flex h-8 w-8 shrink-0 items-center justify-center rounded-full ${m.role === "assistant" ? "bg-gradient-to-br from-primary to-[var(--primary-glow)] text-primary-foreground" : "bg-secondary text-foreground"}`}>
                  {m.role === "assistant" ? <Bot className="h-4 w-4" /> : <User className="h-4 w-4" />}
                </div>
                <div className={`max-w-[80%] rounded-2xl px-4 py-3 text-sm ${m.role === "assistant" ? "bg-secondary text-foreground" : "bg-primary text-primary-foreground"} ${m.isError ? "border border-destructive/30 text-destructive" : ""}`}>
                  {m.analysis ? <AnalysisResult analysis={m.analysis} /> : m.content}
                </div>
              </div>
            ))}
            {isLoading && (
              <div className="flex gap-3">
                <div className="flex h-8 w-8 shrink-0 items-center justify-center rounded-full bg-gradient-to-br from-primary to-[var(--primary-glow)] text-primary-foreground">
                  <Bot className="h-4 w-4" />
                </div>
                <div className="max-w-[80%] rounded-2xl bg-secondary px-4 py-3 text-sm text-muted-foreground">
                  Analyse en cours...
                </div>
              </div>
            )}
          </CardContent>
          <div className="border-t border-border p-4">
            <div className="flex gap-2">
              <Textarea
                value={input}
                onChange={(e) => setInput(e.target.value)}
                onKeyDown={(e) => {
                  if (e.key === "Enter" && !e.shiftKey) {
                    e.preventDefault();
                    void send(input);
                  }
                }}
                placeholder="Posez votre question QHSE..."
                disabled={isLoading}
                className="min-h-[44px] resize-none"
              />
              <Button onClick={() => void send(input)} size="icon" className="h-auto" disabled={isLoading || !input.trim()}>
                <Send className="h-4 w-4" />
              </Button>
            </div>
          </div>
        </Card>
        <div className="space-y-3">
          <div className="flex items-center gap-2 text-sm font-medium text-foreground">
            <Sparkles className="h-4 w-4 text-primary" /> Suggestions
          </div>
          {suggestions.map((s) => (
            <button
              key={s}
              onClick={() => void send(s)}
              disabled={isLoading}
              className="w-full rounded-lg border border-border bg-card p-3 text-left text-sm text-foreground transition-colors hover:border-primary hover:bg-secondary disabled:cursor-not-allowed disabled:opacity-60"
            >
              {s}
            </button>
          ))}
        </div>
      </div>
    </div>
  );
}

function AnalysisResult({ analysis }: { analysis: AiAnalyzeResponse }) {
  return (
    <div className="space-y-3">
      <div>
        <p className="text-xs font-medium uppercase text-muted-foreground">Resume</p>
        <p className="mt-1">{analysis.summary}</p>
      </div>
      <div>
        <p className="text-xs font-medium uppercase text-muted-foreground">Niveau de risque</p>
        <p className="mt-1 font-semibold text-foreground">{analysis.riskLevel}</p>
      </div>
      <AnalysisList title="Actions correctives" items={analysis.correctiveActions} />
      <AnalysisList title="Recommandations" items={analysis.recommendations} />
    </div>
  );
}

function AnalysisList({ title, items }: { title: string; items: string[] }) {
  if (!items.length) return null;

  return (
    <div>
      <p className="text-xs font-medium uppercase text-muted-foreground">{title}</p>
      <ul className="mt-1 list-disc space-y-1 pl-4">
        {items.map((item) => (
          <li key={item}>{item}</li>
        ))}
      </ul>
    </div>
  );
}
