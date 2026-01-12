import type { ReferenceItem } from '../features/referenceItems/referenceItemsSlice';

export type FetchReferenceItemsResponse = {
  items: ReferenceItem[];
  totalCount: number;
};

type FetchReferenceItemsRawResponse = {
  items?: ReferenceItem[];
  totalCount?: number;
};

export const fetchReferenceItems = async (
  page: number,
  pageSize: number,
  search?: string
): Promise<FetchReferenceItemsResponse> => {
  const trimmedSearch = search?.trim();
  const searchQuery = trimmedSearch
    ? `&search=${encodeURIComponent(trimmedSearch)}`
    : '';
  const response = await fetch(
    `/api/ReferenceItems?page=${page}&pageSize=${pageSize}${searchQuery}`
  );
  if (!response.ok) {
    throw new Error('Failed to load reference items.');
  }

  const data = (await response.json()) as FetchReferenceItemsRawResponse;
  return {
    items: data.items ?? [],
    totalCount: data.totalCount ?? 0,
  };
};

export const replaceReferenceItems = async (
  payload: Array<Record<string, string>>
): Promise<void> => {
  const response = await fetch('/api/ReferenceItems', {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(payload),
  });

  if (!response.ok) {
    throw new Error('Не удалось сохранить данные.');
  }
};
