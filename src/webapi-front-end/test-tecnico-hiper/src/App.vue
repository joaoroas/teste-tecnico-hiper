<script setup>
import { ref, onMounted, computed } from 'vue'
import api from './api'
import * as bootstrap from 'bootstrap'

const pedidos = ref([])
const carregando = ref(true)
const mensagemToast = ref('')
const pedidoSelecionado = ref(null);
const textoBusca = ref('')

const editando = ref(false);

const novoPedido = ref({
  customerName: '',
  productName: '',
  amount: 0
});

const pedidosFiltrados = computed(() => {
  if (!textoBusca.value) return pedidos.value;

  const termo = textoBusca.value.toLowerCase();

  return pedidos.value.filter(p =>
    p.orderId?.toString().includes(termo)
  );
});

const mostrarToast = (mensagem, cor = 'bg-danger') => {
  mensagemToast.value = mensagem;

  const toastLiveExample = document.getElementById('liveToast');

  toastLiveExample.className = `toast align-items-center text-white border-0 ${cor}`;

  const toastBootstrap = bootstrap.Toast.getOrCreateInstance(toastLiveExample);
  toastBootstrap.show();
};

const getStatusLabel = (status) => {
  const statusMap = {
    0: { texto: 'Pendente', classe: 'bg-warning text-dark' },
    1: { texto: 'Preparando', classe: 'bg-info' },
    2: { texto: 'Concluído', classe: 'bg-success' }
  }
  return statusMap[status] || { texto: 'Desconhecido', classe: 'bg-secondary' }
}

const prepararEdicao = (pedido) => {

  editando.value = true;

  novoPedido.value = { ...pedido };
};

const limparFormulario = () => {
  editando.value = false;
  novoPedido.value = { customerName: '', productName: '', amount: 0, orderStatus: 0 };
};

const fecharModal = () => {
  const btnFechar = document.querySelector('#modalPedido [data-bs-dismiss="modal"]');
  if (btnFechar) btnFechar.click();
};

const carregarPedidos = async () => {

  try {
    const response = await api.get('/Orders')

    pedidos.value = response.data.data || [];

  } catch (error) {

    console.error("Erro ao buscar pedidos:", error)
    mostrarToast("Erro ao buscar pedidos")

  } finally {
    carregando.value = false
  }
}

const salvarPedido = async () => {
  try {
    let response;

    if (editando.value) {
      response = await api.put('/Order', novoPedido.value);
    } else {
      response = await api.post('/Order', novoPedido.value);
    }

    if (response.data.success) {
      mostrarToast(editando.value ? "Pedido atualizado!" : "Pedido criado!", 'bg-success');
      fecharModal();
      limparFormulario();
      await carregarPedidos();
    }
  } catch (error) {
    const mensagemErro = error.response?.data?.message || "Erro ao processar pedido.";
    mostrarToast(mensagemErro);
  }
};

const excluirPedido = async (id) => {
  try {

    await api.delete(`/Order/${id}`);
    await carregarPedidos();

  } catch (error) {

    const mensagemErro = error.response?.data?.message || "Erro inesperado ao excluir.";
    mostrarToast(mensagemErro);
  }
}

onMounted(carregarPedidos)
</script>

<template>
  <nav class="navbar navbar-dark bg-dark mb-4">
    <div class="container">
      <span class="navbar-brand">🍕 Admin Pedidos</span>
    </div>
  </nav>

  <div class="container">
    <div class="card shadow border-0">
      <div class="card-header bg-white py-3 d-flex justify-content-between align-items-center">
        <h5 class="mb-0 fw-bold">Pedidos Recentes</h5>
        <div class="row mb-3">
          <div class="mb-0 fw-bold">
            <div class="input-group">
              <span class="input-group-text bg-white border-end-0">
                <i class="bi bi-search"></i>
              </span>
              <input v-model="textoBusca" type="text" class="form-control border-start-0" placeholder="Buscar por ID.">
            </div>
          </div>
        </div>
        <button @click="limparFormulario" class="btn btn-success btn-sm" data-bs-toggle="modal"
          data-bs-target="#modalPedido">
          <i class="bi bi-plus-lg"></i> Novo Pedido
        </button>
      </div>
      <div class="card-body p-0">
        <div class="table-responsive">
          <table class="table table-hover align-middle mb-0">
            <thead class="table-light">
              <tr>
                <th>ID</th>
                <th>Cliente</th>
                <th>Produto</th>
                <th>Valor</th>
                <th>Status</th>
                <th>Data</th>
                <th class="text-center">Ações</th>
              </tr>
            </thead>
            <tbody>
              <tr v-if="carregando">
                <td colspan="7" class="text-center py-5">
                  <div class="spinner-border text-primary" role="status"></div>
                </td>
              </tr>

              <tr v-if="!carregando && pedidos.length === 0">
                <td colspan="7" class="text-center py-5 text-muted">
                  <i class="bi bi-inbox d-block fs-2"></i>
                  Nenhum pedido encontrado!
                </td>
              </tr>

              <tr v-for="pedido in pedidosFiltrados" :key="pedido.orderId">
                <td class="fw-bold">#{{ pedido.orderId }}</td>
                <td>{{ pedido.customerName }}</td>
                <td>{{ pedido.productName }}</td>
                <td>R$ {{ pedido.amount.toFixed(2) }}</td>
                <td>
                  <span :class="['badge', getStatusLabel(pedido.orderStatus).classe]">
                    {{ getStatusLabel(pedido.orderStatus).texto }}
                  </span>
                </td>
                <td class="text-muted small">
                  {{ new Date(pedido.createdAt).toLocaleString('pt-BR') }}
                </td>
                <td class="text-end">
                  <button @click="prepararEdicao(pedido)" class="btn btn-sm btn-outline-primary me-2"
                    data-bs-toggle="modal" data-bs-target="#modalPedido">
                    <i class="bi bi-pencil"></i>
                  </button>
                  <button @click="excluirPedido(pedido.orderId)" class="btn btn-sm btn-outline-danger">
                    <i class="bi bi-trash"></i>
                  </button>
                </td>
              </tr>
              <tr v-if="!carregando && pedidosFiltrados.length === 0 && pedidos.length != 0">
                <td colspan="7" class="text-center py-5 text-muted">
                  <i class="bi bi-search d-block fs-2 mb-2"></i>
                  Nenhum resultado encontrado para "{{ textoBusca }}"
                </td>
              </tr>
            </tbody>
          </table>
          <div class="modal fade" id="modalPedido" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog">
              <div class="modal-content">
                <div class="modal-header">
                  <h5 class="modal-title">{{ editando ? 'Editar Pedido #' + novoPedido.orderId : 'Novo Pedido' }}</h5>
                  <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                  <form @submit.prevent="salvarPedido">
                    <div class="mb-3">
                      <label class="form-label">Nome do Cliente</label>
                      <input v-model="novoPedido.customerName" type="text" class="form-control" required>
                    </div>
                    <div class="mb-3">
                      <label class="form-label">Produto</label>
                      <input v-model="novoPedido.productName" type="text" class="form-control" required>
                    </div>
                    <div class="mb-3">
                      <label class="form-label">Valor (R$)</label>
                      <input v-model="novoPedido.amount" type="number" step="0.01" min="0.01" class="form-control"
                        required>
                      <div class="form-text text-danger" v-if="novoPedido.amount <= 0">
                        O valor deve ser maior que zero.
                      </div>
                    </div>
                    <div class="mb-3">
                    </div>
                    <div class="modal-footer px-0 pb-0">
                      <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                      <button type="submit" class="btn btn-primary">Salvar Pedido</button>
                    </div>
                  </form>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
  <div class="toast-container position-fixed bottom-0 end-0 p-3">
    <div id="liveToast" class="toast align-items-center text-white border-0" role="alert" aria-live="assertive"
      aria-atomic="true">
      <div class="d-flex">
        <div class="toast-body">
          {{ mensagemToast }}
        </div>
        <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast"
          aria-label="Close"></button>
      </div>
    </div>
  </div>
</template>