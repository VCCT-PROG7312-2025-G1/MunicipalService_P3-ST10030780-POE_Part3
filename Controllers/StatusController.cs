using Microsoft.AspNetCore.Mvc;
using MunicipalService_P3.Models;
using MunicipalService_P3.DataStructures;   // ✅ Needed for AVL, MinHeap, Graph
using System.Collections.Generic;

namespace MunicipalService_P3.Controllers
{
    public class StatusController : Controller
    {
        // ✅ Core data structures for rubric
        private static readonly AvlTree tree = new AvlTree();
        private static readonly MinHeap heap = new MinHeap();
        private static readonly Graph graph = new Graph();

        // 🔹 Index: show all requests sorted via AVL + top priority from MinHeap
        [HttpGet]
        public IActionResult Index()
        {
            var sorted = tree.InOrder();
            ViewBag.TopPriority = heap.Peek();
            return View(sorted);
        }

        // 🔹 Insert: add new request into AVL, Heap, and Graph
        [HttpPost]
        public IActionResult Insert(ServiceRequest req)
        {
            if (string.IsNullOrWhiteSpace(req.Description))
                ModelState.AddModelError(nameof(req.Description), "Description is required.");

            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Invalid input.";
                return RedirectToAction("Index");
            }

            tree.Insert(req.Id, req);
            heap.Insert(req);
            graph.AddVertex(req.Id);

            TempData["Created"] = $"Request {req.Id} created.";
            return RedirectToAction("Index");
        }

        // 🔹 Search: AVL lookup + BFS dependencies
        [HttpPost]
        public IActionResult Search(int requestId)
        {
            var result = tree.Search(requestId);
            ViewBag.Result = result;

            ViewBag.Dependencies = graph.BFS(requestId);
            ViewBag.TopPriority = heap.Peek();

            return View("Index", tree.InOrder());
        }

        // 🔹 Urgent: extract most urgent request from MinHeap
        [HttpGet]
        public IActionResult Urgent()
        {
            var urgent = heap.ExtractMin();
            return View(urgent);
        }

        // 🔹 MST: compute Minimum Spanning Tree from Graph
        [HttpGet]
        public IActionResult MST()
        {
            var mst = graph.PrimMST();
            return View(mst);
        }

        // 🔹 DFS: depth-first traversal from a given request
        [HttpGet]
        public IActionResult DFS(int startId)
        {
            var order = graph.DFS(startId);
            return View(order);
        }

        // 🔹 Edges: list all graph edges (for visualization/debugging)
        [HttpGet]
        public IActionResult Edges()
        {
            var edges = graph.EdgeList();
            return View(edges);
        }

        // 🔹 SeedDemo: insert sample requests + dependencies for testing
        [HttpGet]
        public IActionResult SeedDemo()
        {
            var demo = new List<ServiceRequest>
            {
                new ServiceRequest { Id=101, Category="Utilities", Description="Water leak", Location="Zone 5", Priority=1, Status="Submitted" },
                new ServiceRequest { Id=102, Category="Roads", Description="Streetlight repair", Location="Zone 2", Priority=2, Status="In Progress" },
                new ServiceRequest { Id=103, Category="Sanitation", Description="Park cleaning", Location="Zone 3", Priority=3, Status="Completed" }
            };

            foreach (var req in demo)
            {
                tree.Insert(req.Id, req);
                heap.Insert(req);
                graph.AddVertex(req.Id);
            }

            graph.AddEdge(101, 102, 2);
            graph.AddEdge(102, 103, 3);

            TempData["Seeded"] = "Demo requests inserted.";
            return RedirectToAction("Index");
        }
    }
}