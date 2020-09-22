using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Financeiro.Scripts {

    class SmoothExpandColapse {

        public SmoothExpandColapse (Panel panel, Orientation orientation, int min_size, int max_size, int step, int step_interval) {
            Container = panel;
            Orientation = orientation;
            MinSize = min_size;
            MaxSize = max_size;
            Step = step;
            StepInterval = step_interval;
        }

        Panel Container { get; }
        Orientation Orientation { get; }
        int MinSize { get; }
        int MaxSize { get; }
        int Step { get; }
        int StepInterval { get; }

        int CurrentSize { get; set; }

        public void Expand () {
            var t = new Thread(() => {
                while (CurrentSize < MaxSize) {
                    CurrentSize += Step;
                    App.Current.Dispatcher.Invoke(() => { 
                        if (Orientation == Orientation.Horizontal) {
                            Container.Width = CurrentSize;
                        } else {
                            Container.Height = CurrentSize;
                        }
                    });
                    Thread.Sleep(StepInterval);
                }
            });
            t.Start();
        }

        public void Colapse () {
            var t = new Thread(() => {
                while (CurrentSize > MinSize) {
                    CurrentSize -= Step;
                    App.Current.Dispatcher.Invoke(() => {
                        if (Orientation == Orientation.Horizontal) {
                            Container.Width = CurrentSize;
                        } else {
                            Container.Height = CurrentSize;
                        }
                    });
                    Thread.Sleep(StepInterval);
                }
            });
            t.Start();
        }

    }
}
