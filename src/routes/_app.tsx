import { createFileRoute, Outlet } from "@tanstack/react-router";
import { AppSidebar } from "@/components/qhse/AppSidebar";
import { Topbar } from "@/components/qhse/Topbar";

export const Route = createFileRoute("/_app")({
  component: AppLayout,
});

function AppLayout() {
  return (
    <div className="flex min-h-screen w-full bg-background">
      <AppSidebar />
      <div className="flex min-w-0 flex-1 flex-col">
        <Topbar />
        <main className="flex-1 overflow-y-auto p-6">
          <Outlet />
        </main>
      </div>
    </div>
  );
}