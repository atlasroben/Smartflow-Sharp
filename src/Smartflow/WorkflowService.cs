﻿/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: https://www.smartflow-sharp.com
 ********************************************************************
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

using Smartflow.Elements;
using Smartflow.Internals;

namespace Smartflow
{
    public class WorkflowService: AbstractWorkflow
    {
        public IWorkflowPersistent<Element> WorkflowPersistent
        {
            get { return base.NodeService; }
        }

        public override string Start(string resourceXml)
        {
            Workflow workflow = XMLServiceFactory.Create(resourceXml);
            var start = workflow.Nodes.Where(n => n.NodeType == WorkflowNodeCategory.Start).FirstOrDefault();
            string instaceID = InstanceService.CreateInstance(start.ID, resourceXml);
            foreach (Node node in workflow.Nodes)
            {
                node.InstanceID = instaceID;
                WorkflowPersistent.Persistent(node);
            }
            return instaceID;
        }
    }
}
