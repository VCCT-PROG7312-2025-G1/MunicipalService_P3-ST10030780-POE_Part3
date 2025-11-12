using MunicipalService_P3.Models;

namespace MunicipalService_P3.Models.DataStructures
{
    public class ServiceRequestTree
    {
        public class Node
        {
            public ServiceRequest Request { get; set; }
            public Node? Left { get; set; }
            public Node? Right { get; set; }

            public Node(ServiceRequest request) => Request = request;
        }

        public Node? Root { get; private set; }

        public void Insert(ServiceRequest request)
        {
            Root = Insert(Root, request);
        }

        private Node Insert(Node? node, ServiceRequest request)
        {
            if (node == null) return new Node(request);
            if (request.RequestId < node.Request.RequestId)
                node.Left = Insert(node.Left, request);
            else
                node.Right = Insert(node.Right, request);
            return node;
        }

        public ServiceRequest? Search(int requestId)
        {
            var current = Root;
            while (current != null)
            {
                if (requestId == current.Request.RequestId) return current.Request;
                current = requestId < current.Request.RequestId ? current.Left : current.Right;
            }
            return null;
        }
    }
}