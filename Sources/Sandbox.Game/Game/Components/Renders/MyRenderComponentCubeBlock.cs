﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sandbox.Common.Components;
using Sandbox.Game.Entities;
using Sandbox.Engine.Utils;
using VRageRender;
using System.Diagnostics;

namespace Sandbox.Game.Components
{
    public class MyRenderComponentCubeBlock : MyRenderComponent
    {
        protected MyCubeBlock m_cubeBlock = null;

        #region overrides
        public override void OnAddedToContainer(MyComponentContainer container)
        {
            base.OnAddedToContainer(container);
            m_cubeBlock = Entity as MyCubeBlock;
        }
        public override void InvalidateRenderObjects(bool sortIntoCullobjects = false)
        {
        }
        public override void AddRenderObjects()
        {
            base.AddRenderObjects();

            if (MyFakes.MANUAL_CULL_OBJECTS)
            {
                var cell = m_cubeBlock.CubeGrid.RenderData.GetCell(m_cubeBlock.PositionComp.GetPosition() * m_cubeBlock.CubeGrid.GridSize);
                if (cell.ParentCullObject == MyRenderProxy.RENDER_ID_UNASSIGNED)
                {
                    cell.RebuildInstanceParts(GetRenderFlags());
                }
                Debug.Assert(cell.ParentCullObject != MyRenderProxy.RENDER_ID_UNASSIGNED, "Somethings wrong, parent cull object should have been created");

                foreach (var renderObjectId in m_renderObjectIDs)
                {
                    MyRenderProxy.SetParentCullObject(renderObjectId, cell.ParentCullObject);
                }
            }
        }
        #endregion
    }
}