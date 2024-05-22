using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poisson_distribution
{
    internal class Generation_queue
    {
        public List<KeyValuePair<int, DateTime?>> queue;
        public List<KeyValuePair<int[], DateTime?>> in_queue1;
        public List<KeyValuePair<int[], DateTime?>> in_queue2;
        public List<KeyValuePair<int[], DateTime?>> in_queue3;
        public List<KeyValuePair<int[], DateTime?>> in_delaychik1;
        public List<KeyValuePair<int[], DateTime?>> in_delaychik2;
        public List<KeyValuePair<int[], DateTime?>> in_delaychik3;
        public List<KeyValuePair<int[], DateTime?>> out_delaychik;
        public Generation_queue() {
            queue = new List<KeyValuePair<int, DateTime?>>();
            in_queue1 = new List<KeyValuePair<int[], DateTime?>>();
            in_queue2 = new List<KeyValuePair<int[], DateTime?>>();
            in_queue3 = new List<KeyValuePair<int[], DateTime?>>();
            in_delaychik1 = new List<KeyValuePair<int[], DateTime?>>();
            in_delaychik2 = new List<KeyValuePair<int[], DateTime?>>();
            in_delaychik3 = new List<KeyValuePair<int[], DateTime?>>();
            out_delaychik = new List<KeyValuePair<int[], DateTime?>>();
        }
        public void add_queue(int number, DateTime? time) {
            var pair = new KeyValuePair<int, DateTime?>(number, time);
            queue.Add(pair);
        }
        public void insert_in_queue(int number, int number_queue, DateTime? time) {
            var pair = new KeyValuePair<int[], DateTime?>([number, number_queue], time);
            if (number_queue == 1) {
                in_queue1.Add(pair);
            }
            if (number_queue == 2) {
                in_queue2.Add(pair);
            }
            if (number_queue == 3) {
                in_queue3.Add(pair);
            }
            queue.RemoveAll(item => item.Key == number);
        }
        public void insert_in_delay(int number, int number_queue, DateTime? time) {
            var pair = new KeyValuePair<int[], DateTime?>([number, number_queue], time);
            if (number_queue == 1) {
                in_delaychik1.Add(pair);
                in_queue1.RemoveAll(item => item.Key[0] == number);
            }
            if (number_queue == 2) {
                in_delaychik2.Add(pair);
                in_queue2.RemoveAll(item => item.Key[0] == number);
            }
            if (number_queue == 3) {
                in_delaychik3.Add(pair);
                in_queue3.RemoveAll(item => item.Key[0] == number);
            }
        }
        public void delete_delay(int number, int number_queue, DateTime? time) {
            var pair = new KeyValuePair<int[], DateTime?>([number, number_queue], time);
            if (number_queue == 1) {
                out_delaychik.Add(pair);
                in_delaychik1.RemoveAll(item => item.Key[0] == number);
            }
            if (number_queue == 2) {
                out_delaychik.Add(pair);
                in_delaychik2.RemoveAll(item => item.Key[0] == number);
            }
            if (number_queue == 3) {
                out_delaychik.Add(pair);
                in_delaychik3.RemoveAll(item => item.Key[0] == number);
            }
        }
    }
}
