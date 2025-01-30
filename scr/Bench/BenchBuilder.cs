using Kompas6API5;
using Kompas6Constants3D;

namespace Bench
{
    /// <summary>
    /// Отвечает за логику построения скамейки в Kompas-3D.
    /// </summary>
    public class BenchBuilder
    {
        /// <summary>
        /// Экземпляр класса BenchWrapper для взаимодействия с API Kompas-3D.
        /// </summary>
        private readonly BenchWrapper _benchWrapper;

        /// <summary>
        /// Конструктор класса. 
        /// </summary>
        /// <param name="wrapper">Экземпляр класса BenchWrapper для взаимодействия с API Kompas-3D.</param>
        public BenchBuilder(BenchWrapper wrapper)
        {
            _benchWrapper = wrapper;
        }

        /// <summary>
        /// Выполняет построение скамьи.
        /// </summary>
        /// <param name="parameters">Параметры скамьи.</param>
        /// <exception cref="Exception">Выбрасывается, если Компас 3D не подключен
        /// или возникли ошибки при взаимодействии с САПР.</exception>
        public void BuildBench(BenchParameters parameters)
        {
            var kompas = _benchWrapper.Kompas;
            if (kompas == null)
            {
                throw new Exception("Kompas-3D не подключен.");
            }

            var doc3D = (ksDocument3D)kompas.Document3D();
            if (!doc3D.Create(false, true))
            {
                throw new Exception("Не удалось создать новый документ Kompas-3D.");
            }

            var part = (ksPart)doc3D.GetPart((short)Part_Type.pTop_Part);
            if (part == null)
            {
                throw new Exception("Не удалось получить часть документа.");
            }

            BuildLegs(part, parameters);
            BuildSeat(part, parameters);
        }

        /// <summary>
        /// Выполняет построение ножек скамьи.
        /// </summary>
        /// <param name="part">Экземпляр класса ksPart для создания нового элемента модели.</param>
        /// <param name="parameters">Параметры скамьи.</param>
        private void BuildLegs(ksPart part, BenchParameters parameters)
        {
            BuildBoard(part, parameters.LegWidth, parameters.LegWidth, parameters.LegLength, 0, 0);
            BuildBoard(
                part,
                parameters.LegWidth,
                parameters.LegWidth,
                parameters.LegLength,
                parameters.BenchLength - parameters.LegWidth,
                0);
        }

        /// <summary>
        /// Выполняет построение сиденья.
        /// </summary>
        /// <param name="part">Экземпляр класса ksPart для создания нового элемента модели.</param>
        /// <param name="parameters">Параметры скамьи.</param>
        private void BuildSeat(ksPart part, BenchParameters parameters)
        {
            var thickness = 20; // Фиксированная толщина сидушки
            BuildBoard(part, parameters.BenchLength, parameters.SeatWidth, thickness, 0, parameters.LegLength);
        }

        /// <summary>
        /// Выполняет построение доски.
        /// </summary>
        /// <param name="part">Экземпляр класса ksPart для создания нового элемента модели.</param>
        /// <param name="boardLength">Длина доски.</param>
        /// <param name="boardWidth">Ширина доски.</param>
        /// <param name="boardThickness">Толщина доски.</param>
        /// <param name="boardOffsetX">Смещение по X.</param>
        /// <param name="boardOffsetY">Смещение по Y.</param>
        private void BuildBoard(
    ksPart part,
    double boardLength,
    double boardThickness, // Толщина теперь идет вторым параметром
    double boardWidth, // А ширина — третьим
    double boardOffsetX,
    double boardOffsetY)
        {
            var sketchEntity = (ksEntity)part.NewEntity((short)Obj3dType.o3d_sketch);
            var sketchDef = (ksSketchDefinition)sketchEntity.GetDefinition();

            sketchDef.SetPlane(part.GetDefaultEntity((short)Obj3dType.o3d_planeXOY));
            sketchEntity.Create();

            var sketchEdit = (ksDocument2D)sketchDef.BeginEdit();
            sketchEdit.ksLineSeg(
                boardOffsetX,
                boardOffsetY,
                boardOffsetX + boardLength,
                boardOffsetY,
                1
            );
            sketchEdit.ksLineSeg(
                boardOffsetX + boardLength,
                boardOffsetY,
                boardOffsetX + boardLength,
                boardOffsetY + boardWidth,
                1
            );
            sketchEdit.ksLineSeg(
                boardOffsetX + boardLength,
                boardOffsetY + boardWidth,
                boardOffsetX,
                boardOffsetY + boardWidth,
                1
            );
            sketchEdit.ksLineSeg(
                boardOffsetX,
                boardOffsetY + boardWidth,
                boardOffsetX,
                boardOffsetY,
                1
            );
            sketchDef.EndEdit();

            MakeExtrusion(part, sketchEntity, boardThickness);
        }

        /// <summary>
        /// Выполняет операцию выдавливания для создания объемного объекта из эскиза.
        /// </summary>
        /// <param name="part">Экземпляр класса ksPart для создания нового элемента модели.</param>
        /// <param name="sketchEntity">Эскиз.</param>
        /// <param name="extrusionDepth">Глубина выдавливания.</param>
        private void MakeExtrusion(ksPart part, ksEntity sketchEntity, double extrusionDepth)
        {
            var extrusionEntity = (ksEntity)part.NewEntity((short)Obj3dType.o3d_baseExtrusion);
            var extrusionDef = (ksBaseExtrusionDefinition)extrusionEntity.GetDefinition();

            extrusionDef.SetSideParam(true, 0, extrusionDepth);
            extrusionDef.SetSketch(sketchEntity);

            extrusionEntity.Create();
        }
    }
}
