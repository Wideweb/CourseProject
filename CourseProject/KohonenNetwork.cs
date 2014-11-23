using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject
{
    class KohonenNetwork
    {
        private readonly Input[] _inputs;
        private readonly Neuron[] _neurons;
        private readonly int _alphabetLen;
        private readonly int _pixelsNumber;

        public KohonenNetwork()
        {
            _alphabetLen = 26;
            _pixelsNumber = 45 * 45;
            _inputs = new Input[_pixelsNumber];
            _neurons = new Neuron[_alphabetLen];
            FieldInputs();
            FieldNeurons();
            CreateNetwork();
            SetZeroExcitations();
        }

        private void FieldInputs()
        {
            for (var i = 0; i < _pixelsNumber; i++)
                _inputs[i] = new Input(_alphabetLen);
        }

        private void FieldNeurons()
        {
            for (var i = 0; i < _alphabetLen; i++)
                _neurons[i] = new Neuron(_pixelsNumber);
        }

        private void CreateNetwork()
        {
            for (var j = 0; j < _pixelsNumber; j++)
            {
                for (var i = 0; i < _alphabetLen; i++)
                {
                    var link = new Link(_neurons[i]);
                    _inputs[j].OutgoingLinks[i] = link;
                    _neurons[i].IncomingLinks[j] = link;
                }
            }
        }

        public int  Parse(int[] input)
        {
            for (var i = 0; i < _inputs.Length; i++)
            {
                var inputNeuron = _inputs[i];
                foreach (var outgoingLink in inputNeuron.OutgoingLinks)
                {
                    outgoingLink.Neuron.Power += outgoingLink.Weight * input[i];
                }
            }
            var maxIndex = FindNeuronWithMaxExcitation();
            SetZeroExcitations();

            return maxIndex;
        }

        private int FindNeuronWithMaxExcitation()
        {
            var maxIndex = 0;
            for (var i = 1; i < _neurons.Length; i++)
            {
                if (_neurons[i].Power > _neurons[maxIndex].Power)
                    maxIndex = i;
            }
            return maxIndex;
        }

        private void SetZeroExcitations()
        {
            foreach (var outputNeuron in _neurons)
            {
                outputNeuron.Power = 0;
            }
        }

        public void Study(int[] input, int correctAnswer)
        {
            var neuron = _neurons[correctAnswer];
            for (var i = 0; i < neuron.IncomingLinks.Length; i++)
            {
                var incomingLink = neuron.IncomingLinks[i];
                incomingLink.Weight = incomingLink.Weight + 0.5 * (input[i] - incomingLink.Weight);
            }
        }


    }
}
